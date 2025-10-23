using Application.UseCases.Auth.DTOs;
using Application.UseCases.Auth.DTOs.Password;
using Application.UseCases.Auth.Service;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Auth.UseCases.Password;

public class SendPasswordResetLinkUseCase
{
    private const string TokenTypeName = "PASSWORD_RESET";
    private readonly IEmailAuthService _emailAuthService;
    private readonly ILogger<SendPasswordResetLinkUseCase> _logger;
    private readonly ISecurityTokenService _securityTokenService;
    private readonly IUserRepository _userRepository;

    public SendPasswordResetLinkUseCase(
        IUserRepository userRepository,
        ISecurityTokenService securityTokenService,
        IEmailAuthService emailAuthService,
        ILogger<SendPasswordResetLinkUseCase> logger)
    {
        _userRepository = userRepository;
        _securityTokenService = securityTokenService;
        _emailAuthService = emailAuthService;
        _logger = logger;
    }

    public async Task<ResponseWithSimplKeyDto> ExecuteAsync(RequestPasswordResetRequestDto request,
        string ipAddress, string userAgent, string languageCode)
    {
        _logger.LogInformation("Demande de réinitialisation de mot de passe pour {Email}", request.Email);

        var user = await _userRepository.GetByEmailAsync(request.Email)
                   ?? throw new BusinessException(ErrorCodes.UserNotFound, "email");

        // Génération + enregistrement + hash
        var generatedTokenResult = await _securityTokenService.ReGenerateAndStoreAsync(
            user.UserId,
            TokenTypeName,
            ipAddress,
            userAgent
        );
        _logger.LogWarning("✅ RawToken: {RawToken}, RawOtp: {RawOtp}", generatedTokenResult.RawToken, generatedTokenResult.CodeOtp);

        if (generatedTokenResult.RawToken is null || generatedTokenResult.CodeOtp is null)
            throw new BusinessException(ErrorCodes.TokenGenerationFailed, "token");
        
        // Envoi email avec lien contenant le token
        _logger.LogInformation("Appel de SendPasswordReseLinkEmailAsync");
        await _emailAuthService.SendPasswordReseLinkEmailAsync(user, generatedTokenResult.RawToken, generatedTokenResult.CodeOtp,ipAddress, userAgent,languageCode);

        _logger.LogInformation("Lien de réinitialisation envoyé à {Email}", user.Email);

        return new ResponseWithSimplKeyDto
        {
            Key = "RESETPASSWORD.EMAIL_SENT"
        };
    }
}