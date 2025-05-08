using Application.UseCases.Auth.DTOs;
using Application.UseCases.Auth.Service;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Auth.UseCases.EmailConfirmation;

public class ResendOtpCodeFromEmailUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly ISecurityTokenService _securityTokenService;
    private readonly ILogger<ResendOtpCodeFromEmailUseCase> _logger;
    private readonly IEmailAuthService _emailAuthService;

    private const string TokenTypeName = "EMAIL_CONFIRMATION";

    public ResendOtpCodeFromEmailUseCase(
        IUserRepository userRepository,
        ISecurityTokenService securityTokenService,
        ILogger<ResendOtpCodeFromEmailUseCase> logger,
        IEmailAuthService emailAuthService)
    {
        _userRepository = userRepository;
        _securityTokenService = securityTokenService;
        _logger = logger;
        _emailAuthService = emailAuthService;
    }

    public async Task<OtpRegenerationMetadata> ExecuteAsync(string email, string ipAddress, string userAgent,string languageCode)
    {
        _logger.LogInformation("Demande de renvoi de code OTP par email pour {Email}", email);

        var user = await _userRepository.GetByEmailAsync(email)
                   ?? throw new BusinessException(ErrorCodes.UserNotFound, "email");

        if (user.IsVerified)
        {
            _logger.LogWarning("Email déjà confirmé pour {Email}", email);
            throw new BusinessException(ErrorCodes.EmailAlreadyConfirmed, "email");
        }

        var (rawOtp,metadata) = await _securityTokenService.RegenerateOtpOnlyAsync(user.UserId, TokenTypeName, ipAddress, userAgent);
        
        await _emailAuthService.SendOtpCodeEmailAsync(user, rawOtp, languageCode);
        


        _logger.LogInformation("Code OTP renvoyé à l'utilisateur {UserId} ({Email})", user.UserId, email);
        return metadata;
    }
}