using Application.UseCases.Auth.DTOs.Password;
using Application.UseCases.Auth.Service;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Infrastructure.Ef.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Auth.UseCases.Password;

public class ResetPasswordUseCase
{
    private readonly IEmailAuthService _emailAuthService;
    private readonly ILogger<ResetPasswordUseCase> _logger;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly ISecurityTokenService _securityTokenService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public ResetPasswordUseCase(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        ISecurityTokenService securityTokenService,
        IRefreshTokenRepository refreshTokenRepository,
        IEmailAuthService emailAuthService,
        ILogger<ResetPasswordUseCase> logger,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        _securityTokenService = securityTokenService ?? throw new ArgumentNullException(nameof(securityTokenService));
        _refreshTokenRepository =
            refreshTokenRepository ?? throw new ArgumentNullException(nameof(refreshTokenRepository));
        _emailAuthService = emailAuthService ?? throw new ArgumentNullException(nameof(emailAuthService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ConfirmPasswordResetResponseDto> ExecuteAsync(
        ConfirmPasswordResetRequestDto request,
        string ipAddress,
        string userAgent,string languageCode)
    {
        // 1. Valider et récupérer le token
        var (token,_) = await _securityTokenService.ValidateTokenAsync(request.Token, "PASSWORD_RESET");
        //  On récupère l’utilisateur lié
        var user = await _userRepository.GetByIdAsync(token.UserId)
                   ?? throw new BusinessException(ErrorCodes.UserNotFound, "userId");

        if (user == null || !string.Equals(user.Email, request.Email, StringComparison.OrdinalIgnoreCase))
            throw new BusinessException(ErrorCodes.UserNotFound, "email");

        // 2. Ne pas accepter le même mot de passe
        if (_passwordHasher.Verify(user.PasswordHash, request.NewPassword))
            throw new BusinessException(ErrorCodes.PasswordResetSameAsOld, "newPassword");

        // 3. Met à jour le mot de passe
        user.PasswordHash = _passwordHasher.Hash(request.NewPassword);
        user.UpdatedAt = DateTime.UtcNow;

        await using var transaction = await _unitOfWork.BeginTransactionAsync();

        try
        {
            await _userRepository.UpdateAsync(user);
            await _securityTokenService.MarkAsUsedAsync(token);
            await _refreshTokenRepository.DeleteAllForUserAsync(user.UserId);

            await _unitOfWork.SaveChangesAsync(); 
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "Erreur lors du reset password pour {Email}", user.Email);
            throw new BusinessException(ErrorCodes.PasswordResetFailed, "email");
        }

        // Email peut être envoyé en dehors de la transaction
        await _emailAuthService.SendPasswordChangedConfirmationEmailAsync(user, ipAddress, userAgent,languageCode);


        return new ConfirmPasswordResetResponseDto
        {
            Key = "RESETPASSWORD.SUCCESS"
        };
    }
}