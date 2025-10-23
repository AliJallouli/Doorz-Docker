using Application.UseCases.Auth.DTOs;
using Application.UseCases.Auth.DTOs.Password;
using Application.UseCases.Auth.Service;
using Application.Validation;
using Domain.Constants;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Infrastructure.Ef.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Auth.UseCases.Password;

public class UpdatePasswordUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ISessionService _sessionService;
    private readonly IEmailAuthService _emailAuthService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdatePasswordUseCase> _logger;
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserActionLogService _userActionLogService;

    public UpdatePasswordUseCase(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        ISessionService sessionService,
        IEmailAuthService emailAuthService,
        IUnitOfWork unitOfWork,
        ILogger<UpdatePasswordUseCase> logger,
        IAuthenticationService authenticationService,
        IUserActionLogService userActionLogService)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository), "Le référentiel utilisateur ne peut pas être null.");
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher), "Le service de hachage de mot de passe ne peut pas être null.");
        _sessionService = sessionService ?? throw new ArgumentNullException(nameof(sessionService), "Le service de session ne peut pas être null.");
        _emailAuthService = emailAuthService ?? throw new ArgumentNullException(nameof(emailAuthService), "Le service d'authentification par email ne peut pas être null.");
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork), "L'unité de travail ne peut pas être null.");
        _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Le logger ne peut pas être null.");
        _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService), "Le service d'authentification ne peut pas être null.");
        _userActionLogService = userActionLogService ?? throw new ArgumentNullException(nameof(userActionLogService), "Le service des journaux d'actions utilisateur ne peut pas être null.");
    }

    public async Task<ResponseWithSimplKeyDto> ExecuteAsync(
        UpdatePasswordRequestDto dto,
        int userId,
        int currentSessionId,
        string ipAddress,
        string userAgent,
        string languageCode)
    {
        _logger.LogInformation("Démarrage de la mise à jour du mot de passe pour l'utilisateur {UserId}", userId);

        // Vérifier la limite de taux
        try
        {
            await _userActionLogService.CanPerformActionAsync(userId, UserActionTypes.PasswordUpdate);
        }
        catch (BusinessException ex) when (ex.Key == ErrorCodes.RateLimitExceeded)
        {
            _logger.LogWarning(
                "Limite de taux dépassée pour la mise à jour du mot de passe de l'utilisateur {UserId}. ExtraData: {ExtraData}",
                userId, ex.ExtraData);
            throw;
        }

        // Vérification des formats
        if (!CommonFormatValidator.ValidatePassword(dto.CurrentPassword))
        {
            _logger.LogWarning(
                "Mot de passe actuel invalide pour la mise à jour du mot de passe de l'utilisateur {UserId}", userId);
            throw new BusinessException(ErrorCodes.ActualPasswordInvalid, "currentPassword");
        }

        if (!CommonFormatValidator
                .ValidatePassword(dto
                    .NewPassword)) // Corrigé : inversion de la condition pour valider le nouveau mot de passe
        {
            _logger.LogWarning(
                "Nouveau mot de passe invalide pour la mise à jour du mot de passe de l'utilisateur {UserId}", userId);
            throw new BusinessException(ErrorCodes.NewPasswordInvalid, "newPassword");
        }

        // Vérification du user et le mot de passe actuel 
        var user = await _userRepository.GetByIdAsync(userId)
                   ?? throw new BusinessException(ErrorCodes.UserNotFound, "userId");
        _logger.LogDebug("Utilisateur {UserId} trouvé pour la mise à jour du mot de passe", userId);

        if (!_passwordHasher.Verify(user.PasswordHash, dto.CurrentPassword))
            throw new BusinessException(ErrorCodes.ActualPasswordInvalid, "currentPassword");
        _logger.LogDebug("Mot de passe actuel vérifié pour l'utilisateur {UserId}", userId);

        // Vérification si le mot de passe reçu est le même qu'en db
        if (_passwordHasher.Verify(user.PasswordHash, dto.NewPassword))
            throw new BusinessException(ErrorCodes.PasswordResetSameAsOld, "newPassword");

        // Update
        user.PasswordHash = _passwordHasher.Hash(dto.NewPassword);
        user.UpdatedAt = DateTime.UtcNow;

        await using var transaction = await _unitOfWork.BeginTransactionAsync();

        try
        {
            await _userRepository.UpdateAsync(user);

            // Update de la table des logs
            var userAgentId = await _authenticationService.ProcessUserAgentAsync(userAgent);
            await _userActionLogService.LogActionAsync(
                userId,
                UserActionTypes.PasswordUpdate,
                userAgentId,
                ipAddress,
                oldValue: "*********",
                newValue: "*********"
            );

            // Révoquer toutes les sessions sauf celle en cours
            await _sessionService.RevokeAllSessionsExceptAsync(userId, currentSessionId, "PasswordChanged");
            _logger.LogInformation(
                "Toutes les sessions sauf la session actuelle {CurrentSessionId} ont été révoquées pour l'utilisateur {UserId}",
                currentSessionId, userId);

            await _unitOfWork.SaveChangesAsync();
            await transaction.CommitAsync();
            _logger.LogInformation("Mot de passe mis à jour avec succès pour l'utilisateur {UserId}", userId);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "Erreur lors de la mise à jour du mot de passe pour l'utilisateur {UserId}", userId);
            throw new BusinessException(ErrorCodes.PasswordUpdateFailed, "update");
        }

        // Envoi de l'email de confirmation
        try
        {
            await _emailAuthService.SendPasswordChangedConfirmationEmailAsync(user, ipAddress, userAgent, languageCode);
            _logger.LogInformation(
                "Email de confirmation de changement de mot de passe envoyé pour l'utilisateur {UserId}", userId);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex,
                "Échec de l'envoi de l'email de confirmation de changement de mot de passe pour l'utilisateur {UserId}",
                userId);
            // Pas de relance d'exception car l'échec de l'email n'est pas critique
        }

        return new ResponseWithSimplKeyDto
        {
            Key = "UPDATEPASSWORD.SUCCESS"
        };
    }
}