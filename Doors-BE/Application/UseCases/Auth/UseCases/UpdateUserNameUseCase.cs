using Application.UseCases.Auth.DTOs;
using Application.UseCases.Auth.Service;
using Application.Validation;
using Domain.Constants;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Infrastructure.Ef.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Auth.UseCases;

public class UpdateUserNameUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateUserNameUseCase> _logger;
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserActionLogService _userActionLogService;

    public UpdateUserNameUseCase(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IUnitOfWork unitOfWork,
        ILogger<UpdateUserNameUseCase> logger,
        IAuthenticationService authenticationService, 
        IUserActionLogService userActionLogService)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository), "Le référentiel utilisateur ne peut pas être null.");
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher), "Le service de hachage de mot de passe ne peut pas être null.");
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork), "L'unité de travail ne peut pas être null.");
        _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Le logger ne peut pas être null.");
        _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService), "Le service d'authentification ne peut pas être null.");
        _userActionLogService = userActionLogService ?? throw new ArgumentNullException(nameof(userActionLogService), "Le service des journaux d'actions utilisateur ne peut pas être null.");
    }

public async Task<ResponseWithSimplKeyDto> ExecuteAsync(UpdateNameRequestDto dto, int userId, int sessionId, string ipAddress, string userAgent, string languageCode)
{
    _logger.LogInformation("Début de la mise à jour du nom pour l'utilisateur {UserId}, nouveau prénom : {NewFirstName}, nouveau nom : {NewLastName}, session : {SessionId}, IP : {IpAddress}", 
        userId, dto.NewFirstName, dto.NewLastName, sessionId, ipAddress);

    // Vérifier la limite de taux
    try
    {
        await _userActionLogService.CanPerformActionAsync(userId, UserActionTypes.NameUpdate);
    }
    catch (BusinessException ex) when (ex.Key == ErrorCodes.RateLimitExceeded)
    {
        _logger.LogWarning("Limite de taux dépassée pour la mise à jour du nom de l'utilisateur {UserId}. ExtraData: {ExtraData}", userId, ex.ExtraData);
        throw; 
    }
    
    // Vérification des formats
    if (!CommonFormatValidator.ValidatePassword(dto.CurrentPassword))
    {
        _logger.LogWarning("Mot de passe actuel vide pour la mise à jour du nom de l'utilisateur {UserId}, session : {SessionId}", userId, sessionId);
        throw new BusinessException(ErrorCodes.ActualPasswordInvalid, "currentPassword");
    }

    if (!CommonFormatValidator.ValidateFirstName(dto.NewFirstName) || !CommonFormatValidator.ValidateLastName(dto.NewLastName))
    {
        _logger.LogWarning("Prénom ou nom vide pour la mise à jour de l'utilisateur {UserId}", userId);
        throw new BusinessException(ErrorCodes.InvalidNameFields, "firstNameLastName");
    }

    // Vérification de l'user et le mot de passe
    var user = await _userRepository.GetByIdAsync(userId);
    if (user == null)
    {
        _logger.LogWarning("Utilisateur {UserId} non trouvé lors de la mise à jour du nom", userId);
        throw new BusinessException(ErrorCodes.UserNotFound, "userId");
    }
    _logger.LogDebug("Utilisateur {UserId} trouvé : {FirstName} {LastName}", userId, user.FirstName, user.LastName);

    if (!_passwordHasher.Verify(user.PasswordHash, dto.CurrentPassword))
    {
        _logger.LogWarning("Mot de passe actuel incorrect pour l'utilisateur {UserId}", userId);
        throw new BusinessException(ErrorCodes.ActualPasswordInvalid, "currentPassword");
    }

    // Vérification des valeurs recu == les vielles valeurs
    var newFirstName = dto.NewFirstName!.Trim();
    var newLastName = dto.NewLastName!.Trim();

    if (user.FirstName == newFirstName && user.LastName == newLastName)
    {
        _logger.LogWarning("Tentative de mise à jour avec les mêmes prénom {NewFirstName} et nom {NewLastName} pour l'utilisateur {UserId}", 
            newFirstName, newLastName, userId);
        throw new BusinessException(ErrorCodes.NamesUnchanged, "firstName,lastName");
    }
    
    // Enregistrement des modification
    var oldValue = $"{user.FirstName} {user.LastName}";
    user.FirstName = newFirstName;
    user.LastName = newLastName;
    user.UpdatedAt = DateTime.UtcNow;

    

    _logger.LogInformation("Mise à jour des informations de l'utilisateur {UserId} : nouveau prénom {NewFirstName}, nouveau nom {NewLastName}", 
        userId, newFirstName, newLastName);

    await using var transaction = await _unitOfWork.BeginTransactionAsync();
    try
    {
        await _userRepository.UpdateAsync(user);
        _logger.LogInformation("Utilisateur {UserId} mis à jour dans la base de données", userId);

        var userAgentId =   await _authenticationService.ProcessUserAgentAsync(userAgent);
        await _userActionLogService.LogActionAsync(
            userId,
            UserActionTypes.NameUpdate,
            userAgentId,
            ipAddress,
            oldValue: oldValue,
            newValue: $"{dto.NewFirstName} {dto.NewLastName}"
        );

        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Changements enregistrés pour l'utilisateur {UserId}", userId);

        await _unitOfWork.CommitAsync();
        _logger.LogInformation("Transaction validée pour la mise à jour du nom de l'utilisateur {UserId}", userId);
    }
    catch (Exception ex)
    {
        await _unitOfWork.RollbackAsync();
        _logger.LogError(ex, "Erreur lors de la mise à jour du nom pour l'utilisateur {UserId}, session : {SessionId}", userId, sessionId);
        throw new BusinessException(ErrorCodes.NameUpdateFailed);
    }
    

    _logger.LogInformation("Mise à jour du nom réussie pour l'utilisateur {UserId}, réponse : ACCOUNT.NAME_UPDATE_SUCCESS", userId);
    return new ResponseWithSimplKeyDto { Key = "NAME_UPDATE_SUCCESS" };
}}