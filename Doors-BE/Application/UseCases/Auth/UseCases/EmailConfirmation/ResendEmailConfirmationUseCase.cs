
using Application.UseCases.Auth.DTOs.EmailConfirmation;
using Application.UseCases.Auth.Service;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Auth.UseCases.EmailConfirmation;

public class ResendEmailConfirmationUseCase
{
    private const string TokenTypeName = "EMAIL_CONFIRMATION";
    private readonly IEmailAuthService _emailAuthService;
    private readonly ILogger<ResendEmailConfirmationUseCase> _logger;
    private readonly ISecurityTokenService _securityTokenService;
    private readonly IUserRepository _userRepository;

    public ResendEmailConfirmationUseCase(
        IUserRepository userRepository,
        ISecurityTokenService securityTokenService,
        IEmailAuthService emailAuthService,
        ILogger<ResendEmailConfirmationUseCase> logger)
    {
        _userRepository = userRepository;
        _securityTokenService = securityTokenService;
        _emailAuthService = emailAuthService;
        _logger = logger;
    }

    public async Task<ResendEmailConfirmationResponseDto> ExecuteAsync(ResendEmailConfirmationRequestDto request,
        string ipAddress, string userAgent,string languageCode)
    {
        _logger.LogInformation("Renvoi d'email de confirmation pour {Email}", request.Email);

        var user = await _userRepository.GetByEmailAsync(request.Email)
                   ?? throw new BusinessException(ErrorCodes.UserNotFound, "email");

        if (user.IsVerified)
            throw new BusinessException(ErrorCodes.EmailAlreadyConfirmed, "email");

        // Génère un nouveau token (vérification rate-limitée incluse)
        var generatedTokenResult = await _securityTokenService.ReGenerateAndStoreAsync(
            user.UserId,
            TokenTypeName,
            ipAddress,
            userAgent);
        if (generatedTokenResult.RawToken is null)
            throw new BusinessException(ErrorCodes.TokenGenerationFailed, "token");

        if (generatedTokenResult.CodeOtp != null)
            await _emailAuthService.SendConfirmationEmailLinkAsync(user, generatedTokenResult.RawToken,
                generatedTokenResult.CodeOtp, languageCode);

        _logger.LogInformation("Email de confirmation renvoyé à {Email}", user.Email);

        return new ResendEmailConfirmationResponseDto();
    }
}