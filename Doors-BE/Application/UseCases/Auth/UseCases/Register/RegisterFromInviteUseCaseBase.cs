using Application.UseCases.Auth.DTOs;
using Application.UseCases.Auth.Service;
using Application.Validation;
using Domain.Constants;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Infrastructure.Ef.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Auth.UseCases.Register;


public abstract class RegisterFromInviteUseCaseBase
{
    private const string TokenTypeName = "EMAIL_CONFIRMATION";
    private readonly IAuthenticationService _authService;
    private readonly IEntityTypeRepository _entityTypeRepository;
    private readonly IEntityUserRepository _entityUserRepository;
    private readonly ISuperadminInvitationEntityRepository _invitationEntityRepository;
    private readonly ISuperadminInvitationRepository _invitationRepository;
    private readonly ILogger<RegisterFromInviteUseCaseBase> _logger;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IRoleRepository _roleRepository;
    private readonly ISecurityTokenService _securityTokenService;
    private readonly ISuperRoleRepository _superRoleRepository;
    private readonly ITokenHasher _tokenHasher;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IEmailAuthService _emailAuthService;
    private readonly ILegalConsentService _legalConsentService;

    public RegisterFromInviteUseCaseBase(
        IUserRepository userRepository,
        ISuperadminInvitationRepository invitationRepository,
        ISuperadminInvitationEntityRepository invitationEntityRepository,
        IEntityUserRepository entityUserRepository,
        IPasswordHasher passwordHasher,
        IEntityTypeRepository entityTypeRepository,
        IRoleRepository roleRepository,
        ISuperRoleRepository superRoleRepository,
        IUnitOfWork unitOfWork,
        IAuthenticationService authService,
        ILogger<RegisterFromInviteUseCaseBase> logger,
        IEmailAuthService emailAuthService,
        ISecurityTokenService securityTokenService,
        ITokenHasher tokenHasher,
        ILegalConsentService legalConsentService)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository),
            "Le référentiel utilisateur ne peut pas être null.");
        _invitationRepository = invitationRepository ?? throw new ArgumentNullException(nameof(invitationRepository),
            "Le référentiel des invitations ne peut pas être null.");
        _invitationEntityRepository = invitationEntityRepository ??
                                      throw new ArgumentNullException(nameof(invitationEntityRepository),
                                          "Le référentiel des entités d'invitation ne peut pas être null.");
        _entityUserRepository = entityUserRepository ?? throw new ArgumentNullException(nameof(entityUserRepository),
            "Le référentiel des entités utilisateur ne peut pas être null.");
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher),
            "Le service de hachage de mot de passe ne peut pas être null.");
        _entityTypeRepository = entityTypeRepository ?? throw new ArgumentNullException(nameof(entityTypeRepository),
            "Le référentiel des types d'entités ne peut pas être null.");
        _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository),
            "Le référentiel des rôles ne peut pas être null.");
        _superRoleRepository = superRoleRepository ?? throw new ArgumentNullException(nameof(superRoleRepository),
            "Le référentiel des super rôles ne peut pas être null.");
        _unitOfWork = unitOfWork ??
                      throw new ArgumentNullException(nameof(unitOfWork), "L'unité de travail ne peut pas être null.");
        _authService = authService ?? throw new ArgumentNullException(nameof(authService),
            "Le service d'authentification ne peut pas être null.");
        _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Le logger ne peut pas être null.");
        _emailAuthService = emailAuthService;
        _securityTokenService = securityTokenService;
        _tokenHasher = tokenHasher;
        _legalConsentService = legalConsentService;
    }

    public virtual async Task<AuthResponseDto> ExecuteAsync(RegisterFromInviteRequestDto request, string ipAddress,
        string userAgent,string languageCode)
    {
        _logger.LogInformation("Début de l'inscription via invitation pour l'email {Email}", request.Email);
       
        // Vérification des formats
        if (!CommonFormatValidator.ValidateEmail(request.Email))
        {
            _logger.LogWarning("format de l'email invalide  pour enregistrer  l'utilisateur");
            throw new BusinessException(ErrorCodes.InvalidEmailFormat, "password");
        }

        if (!CommonFormatValidator.ValidatePassword(request.Password))
        {
            _logger.LogWarning("format du mot de passe invalide  pour enregistrer  l'utilisateur");
            throw new BusinessException(ErrorCodes.PasswordInvalid, "password");
        }

        if (!CommonFormatValidator.ValidateFirstName(request.FirstName) ||
            !CommonFormatValidator.ValidateLastName(request.LastName))
        {
            _logger.LogWarning("format du Prénom ou du Nom invalide  pour enregistrer  l'utilisateur");
            throw new BusinessException(ErrorCodes.InvalidNameFields, "firstNameLastName ");
        }

        // Récupère l'invitation à partir du token fourni
        var hashedToken = _tokenHasher.HashToken(request.InvitationToken);
        var invitation = await _invitationRepository.GetByTokenAsync(hashedToken);
        if (invitation == null || invitation.Used || invitation.ExpiresAt < DateTime.UtcNow ||
            invitation.Email != request.Email)
        {
            _logger.LogWarning("Invitation invalide ou expirée pour le token {Token} et l'email {Email}",
                request.InvitationToken, request.Email);
            throw new BusinessException(ErrorCodes.InvitationInvalid, "invitationToken");
        }

        _logger.LogDebug("Invitation valide récupérée avec ID {InvitationId}", invitation.SuperadminInvitationId);

        // Vérifie les détails de l'invitation (entité et rôle)
        var invitationEntity =
            await _invitationEntityRepository.GetByInvitationIdAsync(invitation.SuperadminInvitationId);
        if (invitationEntity == null || invitationEntity.EntityId != request.EntityId ||
            invitationEntity.RoleId != request.RoleId)
        {
            _logger.LogWarning("Détails de l'invitation incohérents pour InvitationId {InvitationId}",
                invitation.SuperadminInvitationId);
            throw new BusinessException(ErrorCodes.InvitationDetailsMismatch);
        }

        _logger.LogDebug("Détails de l'invitation validés pour EntityId {EntityId} et RoleId {RoleId}",
            request.EntityId, request.RoleId);

        // Vérifie si un utilisateur avec cet email existe déjà
        var existingUser = await _userRepository.GetByEmailAsync(request.Email);
        if (existingUser != null)
        {
            _logger.LogWarning("Utilisateur existant détecté pour l'email {Email}", request.Email);
            throw new BusinessException(ErrorCodes.UserAlreadyExists, "email");
        }


        // Valide le type d'entité spécifié dans la requête
        var entityType = await _entityTypeRepository.GetByIdAsync(request.EntityTypeId);
        if (entityType == null)
        {
            _logger.LogError("Type d'entité invalide pour EntityTypeId {EntityTypeId}", request.EntityTypeId);
            throw new BusinessException(ErrorCodes.EntityTypeInvalid, "entityTypeId");
        }

        _logger.LogDebug("Type d'entité validé : {EntityTypeName}", entityType.Name);

        // Restreint ce cas d'utilisation aux entités de type "Company" ou "Institution"
        if (entityType.Name != "Company" && entityType.Name != "Institution")
        {
            _logger.LogError("Type d'entité {EntityTypeName} non supporté pour ce use case", entityType.Name);
            throw new BusinessException(ErrorCodes.EntityTypeUnsupported, "entityTypeId");
        }


        // Récupère et valide le rôle spécifié (peut être surchargé par les classes dérivées)
        var role = await _roleRepository.GetByIdAsync(request.RoleId);
        await ValidateRoleAsync(role, request);
        _logger.LogDebug("Rôle validé pour RoleId {RoleId}", request.RoleId);

        // Crée un nouvel utilisateur avec les données de la requête
        var user = new Users
        {
            Email = request.Email,
            PasswordHash = _passwordHasher.Hash(request.Password), 
            FirstName = request.FirstName,
            LastName = request.LastName,
            SuperRoleId = await _superRoleRepository.GetSuperRoleIdAsync("Others"), 
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsVerified = false 
        };

        // Crée une association entre l'utilisateur et l'entité
        var entityUser = new EntityUser
        {
            EntityId = request.EntityId,
            RoleId = request.RoleId,
            CreatedAt = DateTime.UtcNow
        };

        // Enregistrement de l'utilisateur
        AuthResponseDto authResponse;
        await using var transaction = await _unitOfWork.BeginTransactionAsync(); 
        try
        {
            // Ajoute l'utilisateur à la base de données
            await _userRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync(); 
            _logger.LogDebug("Utilisateur créé avec ID {UserId}", user.UserId);
            
            user.CreatedBy = user.UserId;
            await _userRepository.UpdateAsync(user);
            
            // Récupération de l'userAgent
            var userAgentId = await _authService.ProcessUserAgentAsync(userAgent);
            
            // Enregistrement des documents légaux accépté
            await _legalConsentService.RegisterPrivacyPolicyConsentAsync(user, ipAddress, userAgentId,request.LegalConsents);

            // Génération du token du lien et de l'otp pour la confirmation de l'email
            var generatedTokenResult = await _securityTokenService.GenerateAndStoreAsync(
                user.UserId,
                TokenTypeName,
                ipAddress,
                userAgent
            );


            // Associe l'ID de l'utilisateur à l'entité
            entityUser.UserId = user.UserId;
            entityUser.CreatedAt = DateTime.UtcNow;
            await _entityUserRepository.AddAsync(entityUser);
            _logger.LogDebug("Association EntityUser créée pour UserId {UserId} et EntityId {EntityId}", user.UserId,
                request.EntityId);

            // Marque l'invitation comme utilisée
            invitation.Used = true;
            await _invitationRepository.UpdateAsync(invitation);
            _logger.LogDebug("Invitation marquée comme utilisée pour InvitationId {InvitationId}",
                invitation.SuperadminInvitationId);

            // Recharge l'utilisateur avec ses relations pour l'authentification
            user = await _userRepository.GetByEmailAsync(user.Email)
                   ?? throw new BusinessException(ErrorCodes.UserCreationFailed);

            _logger.LogDebug("Utilisateur rechargé avec succès pour l'email {Email}", user.Email);

            // Détermine l'URL de redirection en fonction du type d'entité
            var redirectUrl = user.EntityUser?.Entity.EntityType.Name == "Company"
                ? $"/company/{user.EntityUser.EntityId}/dashboard"
                : $"/institution/{user.EntityUser?.EntityId}/dashboard";
            _logger.LogDebug("URL de redirection générée : {RedirectUrl}", redirectUrl);

            _logger.LogInformation("Début  de l'auto-login pour l'utilisateur {UserId} depuis l'IP {IpAddress}",
                user.UserId, ipAddress);

            authResponse = await _authService.PerformAutoLoginAsync(
                user,
                ipAddress ,
                GetSuccessMessage(redirectUrl),
                userAgent,
                false,
                SessionOpeningReasons.Registration
            );
            _logger.LogDebug("Connexion automatique effectuée pour l'utilisateur {UserId}", user.UserId);
            
            // Vérification du token du lien et de l'otp pour la confirmation de l'émail et l'envoie du mail
            if (generatedTokenResult.RawToken is null)
                throw new BusinessException(ErrorCodes.TokenGenerationFailed, "token");
            if (generatedTokenResult.CodeOtp != null)
                await _emailAuthService.SendConfirmationEmailLinkAsync(user, generatedTokenResult.RawToken,
                    generatedTokenResult.CodeOtp, languageCode);

            // Persiste toutes les modifications dans la base de données
            await _unitOfWork.SaveChangesAsync();
            await transaction.CommitAsync(); // Valide la transaction
            _logger.LogInformation("Inscription via invitation réussie pour l'utilisateur {UserId}", user.UserId);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "Échec de l'inscription via invitation pour l'email {Email}, rollback effectué",
                request.Email);
            throw;
        }

        return authResponse;
    }

    /// <summary>
    ///     Valide le rôle spécifié dans la requête d'inscription.
    ///     Peut être surchargé par les classes dérivées pour ajouter des règles spécifiques.
    /// </summary>
    /// <param name="role">Le rôle à valider.</param>
    /// <param name="request">Les données d'inscription fournies via l'invitation.</param>
    /// <returns>Une tâche représentant l'opération asynchrone.</returns>
    /// <exception cref="InvalidOperationException">Levée si le rôle est invalide (peut être implémenté dans les dérivées).</exception>
    protected virtual Task ValidateRoleAsync(Role? role, RegisterFromInviteRequestDto request)
    {
        return Task.CompletedTask; // Pas de validation par défaut, peut être surchargé
    }

    /// <summary>
    ///     Fournit un message de succès personnalisé avec l'URL de redirection.
    ///     Peut être surchargé par les classes dérivées pour personnaliser le message.
    /// </summary>
    /// <param name="redirectUrl">L'URL de redirection après inscription.</param>
    /// <returns>Un message de succès incluant l'URL de redirection.</returns>
    protected virtual string GetSuccessMessage(string redirectUrl)
    {
        return $"Inscription réussie. Redirection vers {redirectUrl}";
    }
}