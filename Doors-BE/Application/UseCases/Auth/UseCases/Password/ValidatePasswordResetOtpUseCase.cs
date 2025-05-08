using Application.UseCases.Auth.DTOs.Password;
using Application.UseCases.Auth.Service;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.UseCases.Auth.UseCases.Password;

public class ValidatePasswordResetOtpUseCase
{
    private const string TokenTypeName = "PASSWORD_RESET";
    private readonly ISecurityTokenService _securityTokenService;
    private readonly IUserRepository _userRepository;

    public ValidatePasswordResetOtpUseCase(ISecurityTokenService securityTokenService, IUserRepository userRepository)
    {
        _securityTokenService = securityTokenService ?? throw new ArgumentNullException(nameof(securityTokenService));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<ValidatePasswordResetTokenResponseDto> ExecuteAsync(ValidatePasswordResetTokenAndOtpRequestDto request)
    {
        // 🔒 Validation du token via le service centralisé
        var (token,_) = await _securityTokenService.ValidateTokenAndOtpCodeAsync(request.Token, request.Otp, TokenTypeName);

        // On récupère l’utilisateur lié
        var user = await _userRepository.GetByIdAsync(token.UserId)
                   ?? throw new BusinessException(ErrorCodes.UserNotFound, "userId");


        return new ValidatePasswordResetTokenResponseDto
        {
            Key = "RESETPASSWORD.TOKEN_VALID",
            Email = user.Email
        };
    }
}