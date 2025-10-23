using Application.UseCases.Auth.DTOs;
using Application.UseCases.Auth.DTOs.EmailUser;
using Application.UseCases.Auth.Service;
using Application.Validation;
using Domain.Constants;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Infrastructure.Ef.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Auth.UseCases.EmailUser;

public class UpdateEmailUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailAuthService _emailAuthService;
    private readonly ILogger<UpdateEmailUseCase> _logger;
    private readonly ISessionService _sessionService;
    private readonly ISecurityTokenService _securityTokenService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserActionLogService _userActionLogService;

    public UpdateEmailUseCase(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IEmailAuthService emailAuthService,
        ILogger<UpdateEmailUseCase> logger,
        ISessionService sessionService,
        ISecurityTokenService securityTokenService,
        IPasswordHasher passwordHasher,
        IAuthenticationService authenticationService,
        IUserActionLogService userActionLogService)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _emailAuthService = emailAuthService;
        _logger = logger;
        _sessionService = sessionService;
        _securityTokenService = securityTokenService;
        _passwordHasher = passwordHasher;
        _authenticationService = authenticationService;
        _userActionLogService = userActionLogService;
    }

    public async Task<ResponseWithSimplKeyDto> ExecuteAsync(
        UpdateEmailRequestDto dto,
        int userId,
        int currentSessionId,
        string ipAddress,
        string userAgent,
        string languageCode)
    {
        _logger.LogInformation(
            "Début de la mise à jour de l'email pour l'utilisateur {UserId}, nouvelle email : {NewEmail}", userId,
            dto.NewEmail);

        // Vérifier la limite de taux
        try
        {
            await _userActionLogService.CanPerformActionAsync(userId, UserActionTypes.EmailUpdate);
        }
        catch (BusinessException ex) when (ex.Key == ErrorCodes.RateLimitExceeded)
        {
            _logger.LogWarning(
                "Limite de taux dépassée pour la mise à jour de l'email de l'utilisateur {UserId}. ExtraData: {ExtraData}",
                userId, ex.ExtraData);
            throw;
        }

        // Vérification du format du mot de passe et du nouvel email
        if (!CommonFormatValidator.ValidatePassword(dto.CurrentPassword))
        {
            _logger.LogWarning("Mot de passe actuel invalide pour la mise à jour de l'email de l'utilisateur {UserId}",
                userId);
            throw new BusinessException(ErrorCodes.ActualPasswordInvalid, "currentPassword");
        }

        var newEmail = dto.NewEmail.Trim().ToLowerInvariant();

        if (!CommonFormatValidator.ValidateEmail(newEmail))
        {
            _logger.LogWarning("Tentative de mise à jour avec un email invalide pour l'utilisateur {UserId}", userId);
            throw new BusinessException(ErrorCodes.InvalidNewEmail, "newEmail");
        }

        // Vérification de l'user et le mot de passe en db
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            _logger.LogWarning("Utilisateur {UserId} non trouvé lors de la mise à jour de l'email", userId);
            throw new BusinessException(ErrorCodes.UserNotFound, "userId");
        }

        _logger.LogDebug("Utilisateur {UserId} trouvé : {Email}", userId, user.Email);

        if (!_passwordHasher.Verify(user.PasswordHash, dto.CurrentPassword))
            throw new BusinessException(ErrorCodes.ActualPasswordInvalid, "currentPassword");
        _logger.LogDebug("Mot de passe actuel vérifié pour l'utilisateur {UserId}", userId);

        // Vérification de la nouvelle adresse email == la vieille adresse email
        if (string.Equals(user.Email, newEmail, StringComparison.OrdinalIgnoreCase))
        {
            _logger.LogWarning("Tentative de mise à jour avec le même email {NewEmail} pour l'utilisateur {UserId}",
                newEmail, userId);
            throw new BusinessException(ErrorCodes.EmailUnchanged, "newEmail");
        }

        // Vérification si le nouvel email est déjà utilisé
        var emailInUse = await _userRepository.GetByEmailAsync(newEmail);
        if (emailInUse is not null && emailInUse.UserId != user.UserId)
        {
            _logger.LogWarning("L'email {NewEmail} est déjà utilisé par un autre utilisateur", newEmail);
            throw new BusinessException(ErrorCodes.EmailAlreadyUsed, "newEmail");
        }

        // Update
        var oldEmail = user.Email;
        user.Email = newEmail;
        user.IsVerified = false;
        user.UpdatedAt = DateTime.UtcNow;
        _logger.LogInformation(
            "Mise à jour des informations de l'utilisateur {UserId} : nouvel email {NewEmail}, non vérifié", userId,
            newEmail);

        // Générer le token de confirmation après MAJ de l'email
        var generatedTokenResult = await _securityTokenService.GenerateAndStoreAsync(
            user.UserId,
            "EMAIL_CONFIRMATION",
            ipAddress,
            userAgent
        );

        if (generatedTokenResult.RawToken is null)
        {
            _logger.LogError("Échec de la génération du token pour l'utilisateur {UserId}", userId);
            throw new BusinessException(ErrorCodes.TokenGenerationFailed, "token");
        }

        _logger.LogDebug("Token généré pour l'utilisateur {UserId}, type : EMAIL_CONFIRMATION", userId);

        await using var transaction = await _unitOfWork.BeginTransactionAsync();
        _logger.LogDebug("Début de la transaction pour la mise à jour de l'email de l'utilisateur {UserId}", userId);

        try
        {
            await _userRepository.UpdateAsync(user);
            _logger.LogInformation("Utilisateur {UserId} mis à jour dans la base de données", userId);

            // Update de la table des logs
            var userAgentId = await _authenticationService.ProcessUserAgentAsync(userAgent);
            await _userActionLogService.LogActionAsync(
                userId,
                UserActionTypes.EmailUpdate,
                userAgentId,
                ipAddress,
                oldValue: oldEmail,
                newValue: newEmail
            );

            // Invalider les sessions de l'utilisateur sauf celle d'où se fait la mise à jour
            await _sessionService.RevokeAllSessionsExceptAsync(userId, currentSessionId, "EmailChanged");
            _logger.LogInformation("Sessions révoquées pour l'utilisateur {UserId}, sauf la session {CurrentSessionId}",
                userId, currentSessionId);

            await _unitOfWork.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "Erreur lors de la mise à jour de l'email de l'utilisateur {UserId}", userId);
            throw new BusinessException(ErrorCodes.EmailUpdateFailed, "update");
        }

        // Envoi de l'email
        if (generatedTokenResult.CodeOtp != null)
        {
            try
            {
                await _emailAuthService.SendEmailUpdatedConfirmationEmailAsync(
                    user,
                    generatedTokenResult.RawToken,
                    generatedTokenResult.CodeOtp,
                    languageCode
                );
                _logger.LogInformation("Email de confirmation envoyé à {NewEmail} pour l'utilisateur {UserId}",
                    newEmail, userId);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Échec de l'envoi de l'email de confirmation pour l'utilisateur {UserId}",
                    userId);
                // Pas de relance d'exception car l'échec de l'email n'est pas critique
            }
        }
        else
        {
            _logger.LogError(
                "Échec de la génération du CodeOtp pour l'envoi de l'email de confirmation à l'utilisateur {UserId}",
                userId);
        }

        _logger.LogInformation("Mise à jour de l'email réussie pour l'utilisateur {UserId}", userId);
        return new ResponseWithSimplKeyDto
        {
            Key = "UPDATEEMAIL.SUCCESS"
        };
    }
}