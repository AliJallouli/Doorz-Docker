using Application.UseCases.Auth.DTOs;
using Application.UseCases.Auth.DTOs.EmailConfirmation;
using Application.UseCases.Auth.Service;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.UseCases.Auth.UseCases.EmailConfirmation;

public class ValidateTokenForEmailConfirmationUseCase
{
    private const string TokenTypeName = "EMAIL_CONFIRMATION";
    private readonly ISecurityTokenService _securityTokenService;
    private readonly IUserRepository _userRepository;

    public ValidateTokenForEmailConfirmationUseCase(ISecurityTokenService securityTokenService, IUserRepository userRepository)
    {
        _securityTokenService = securityTokenService ?? throw new ArgumentNullException(nameof(securityTokenService));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<ConfirmEmailResponseDto> ExecuteAsync(ConfirmEmailRequestDto request)
    {
        SecurityToken token;
        OtpRegenerationMetadata metadata;
        if (string.IsNullOrWhiteSpace(request.Token))
        {
            throw new BusinessException(ErrorCodes.MissingConfirmationTokenOrOtp, "email confirmation");
        }
        try
        {
            (token,metadata) = await _securityTokenService.ValidateTokenAsync(request.Token,TokenTypeName);
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

        // On récupère l’utilisateur lié
        var user = await _userRepository.GetByIdAsync(token.UserId)
                   ?? throw new BusinessException(ErrorCodes.UserNotFound, "userId");


        return new ConfirmEmailResponseDto
        {
            Key = "EMAILCONFIRMATION.TOKEN_VALID",
            Metadata = metadata
        };
    }
}