
using Application.UseCases.Auth.DTOs.EmailUser;
using Application.UseCases.Auth.Service;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Infrastructure.Ef.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Auth.UseCases.EmailUser;

public class ConfirmEmaiUseCase
{
    private const string TokenTypeName = "EMAIL_CONFIRMATION";
    private readonly ILogger<ConfirmEmaiUseCase> _logger;
    private readonly ISecurityTokenService _securityTokenService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;


    public ConfirmEmaiUseCase(
        IUserRepository userRepository,
        ISecurityTokenService securityTokenService, IUnitOfWork unitOfWork, ILogger<ConfirmEmaiUseCase> logger)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _securityTokenService = securityTokenService ?? throw new ArgumentNullException(nameof(securityTokenService));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<ConfirmEmailResponseDto> ExecuteAsync(ConfirmEmailRequestDto request)
    {
        SecurityToken token;
        if (string.IsNullOrWhiteSpace(request.Token) || string.IsNullOrWhiteSpace(request.CodeOtp))
        {
            throw new BusinessException(ErrorCodes.MissingConfirmationTokenOrOtp, "email confirmation");
        }
        
        try
        {
            (token,_) = await _securityTokenService.ValidateTokenAndOtpCodeAsync(request.Token, request.CodeOtp,TokenTypeName);
        }
        catch (BusinessException ex) when (ex.Key == ErrorCodes.TokenAlreadyUsed ||
        ex.Key == ErrorCodes.TokenInvalid)
        {
            var usedToken = await _securityTokenService.FindByRawTokenAsync(request.Token, TokenTypeName);
            if (usedToken is not null)
            {
                var userConfirmed = await _userRepository.GetByIdAsync(usedToken.UserId);
                if (userConfirmed is not null && userConfirmed.IsVerified)
                {
                    throw new BusinessException(ErrorCodes.EmailAlreadyConfirmed, "userId");
                }
            }

            throw;
            
        }
        

        var user = await _userRepository.GetByIdAsync(token.UserId)
                   ?? throw new BusinessException(ErrorCodes.UserNotFound, "userId");

        if (user.IsVerified)
            throw new BusinessException(ErrorCodes.EmailAlreadyConfirmed, "userId");

        user.IsVerified = true;
        user.UpdatedAt = DateTime.UtcNow;
        user.UpdatedBy = user.UserId;

        await using var transaction = await _unitOfWork.BeginTransactionAsync();

        try
        {
            await _userRepository.UpdateAsync(user);
            await _securityTokenService.MarkAsUsedAsync(token);

            await _unitOfWork.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la confirmation d'email");
            await transaction.RollbackAsync();
            throw new BusinessException(ErrorCodes.EmailConfirmationFailed, "userId");
        }


        return new ConfirmEmailResponseDto
        {
            Key = "CONFIRMEMAIL.SUCCESS"
        };
    }
    
 
}