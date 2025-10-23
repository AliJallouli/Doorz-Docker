using Application.UseCases.Auth.DTOs;
using Application.UseCases.Auth.Service;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Auth.UseCases.EmailUser;

public class ResendOtpCodeUseCase
{
    private const string TokenTypeName = "EMAIL_CONFIRMATION";
    private readonly IUserRepository _userRepository;
    private readonly ISecurityTokenService _securityTokenService;
    private readonly IEmailAuthService _emailAuthService;
    private readonly ILogger<ResendOtpCodeUseCase> _logger;

    public ResendOtpCodeUseCase(
        IUserRepository userRepository,
        ISecurityTokenService securityTokenService,
        IEmailAuthService emailAuthService,
        ILogger<ResendOtpCodeUseCase> logger)
    {
        _userRepository = userRepository;
        _securityTokenService = securityTokenService;
        _emailAuthService = emailAuthService;
        _logger = logger;
    }

    public async Task<OtpRegenerationMetadata> ExecuteAsync(int userId,string ipAddress, string userAgent,string languageCode)
    {
        var user = await _userRepository.GetByIdAsync(userId)
                   ?? throw new BusinessException(ErrorCodes.UserNotFound, "user");

        if (user.IsVerified)
            throw new BusinessException(ErrorCodes.EmailAlreadyConfirmed, "email");

        // Récupère le token actif
        var tokens = await _securityTokenService.GetActiveTokensForUserAsync(userId, TokenTypeName);
        var token = tokens.FirstOrDefault()
                    ?? throw new BusinessException(ErrorCodes.TokenNotFound, "token");

        if (!token.TokenType.CodeOtpRequired || token.TokenHash == null)
            throw new BusinessException(ErrorCodes.OtpNotAvailableForThisToken, "token");

        // Génère un nouveau code OTP uniquement
        var (newOtp,metadata) = await _securityTokenService.RegenerateOtpOnlyAsync(
            userId,
            TokenTypeName,
            ipAddress,
            userAgent
        );


        // Envoie uniquement le code OTP par email
        await _emailAuthService.SendOtpCodeEmailAsync(user, newOtp, languageCode);

        _logger.LogInformation("Nouveau code OTP envoyé à {Email}", user.Email);
        return metadata;
    }
}