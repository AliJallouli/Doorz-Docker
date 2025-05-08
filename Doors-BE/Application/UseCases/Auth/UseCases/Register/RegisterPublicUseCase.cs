using Application.UseCases.Auth.DTOs;
using Application.UseCases.Auth.Service;
using Application.Validation;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Infrastructure.Ef.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Auth.UseCases.Register;

public class RegisterPublicUseCase
{
    private const string TokenTypeName = "EMAIL_CONFIRMATION";
    private readonly IAuthenticationService _authService;
    private readonly IEmailAuthService _emailAuthService;
    private readonly IEntityRepository _entityRepository;
    private readonly IEntityTypeRepository _entityTypeRepository;
    private readonly IEntityUserRepository _entityUserRepository;
    private readonly ILandLordRepository _landlordRepository;
    private readonly ILogger<RegisterPublicUseCase> _logger;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IRoleRepository _roleRepository;
    private readonly ISecurityTokenService _securityTokenService;
    private readonly IStudentRepository _studentRepository;
    private readonly ISuperRoleRepository _superRoleRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly ILegalConsentService _legalConsentService;

    public RegisterPublicUseCase(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        ISuperRoleRepository superRoleRepository,
        IEntityUserRepository entityUserRepository,
        IRoleRepository roleRepository,
        IStudentRepository studentRepository,
        ILandLordRepository landlordRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IEntityRepository entityRepository,
        IEntityTypeRepository entityTypeRepository,
        IAuthenticationService authService,
        ILogger<RegisterPublicUseCase> logger,
        IEmailAuthService emailAuthService,
        ISecurityTokenService securityTokenService,
        ILegalConsentService legalConsentService)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository),
            "Le référentiel utilisateur ne peut pas être null.");
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher),
            "Le service de hachage de mot de passe ne peut pas être null.");
        _superRoleRepository = superRoleRepository ?? throw new ArgumentNullException(nameof(superRoleRepository),
            "Le référentiel des super rôles ne peut pas être null.");
        _entityUserRepository = entityUserRepository ?? throw new ArgumentNullException(nameof(entityUserRepository),
            "Le référentiel des entités utilisateur ne peut pas être null.");
        _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository),
            "Le référentiel des rôles ne peut pas être null.");
        _studentRepository = studentRepository ?? throw new ArgumentNullException(nameof(studentRepository),
            "Le référentiel des étudiants ne peut pas être null.");
        _landlordRepository = landlordRepository ?? throw new ArgumentNullException(nameof(landlordRepository),
            "Le référentiel des bailleurs privés ne peut pas être null.");
        _unitOfWork = unitOfWork ??
                      throw new ArgumentNullException(nameof(unitOfWork), "L'unité de travail ne peut pas être null.");
        _mapper = mapper ??
                  throw new ArgumentNullException(nameof(mapper), "Le service de mappage ne peut pas être null.");
        _entityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository),
            "Le référentiel des entités ne peut pas être null.");
        _entityTypeRepository = entityTypeRepository ?? throw new ArgumentNullException(nameof(entityTypeRepository),
            "Le référentiel des types d'entités ne peut pas être null.");
        _authService = authService ?? throw new ArgumentNullException(nameof(authService),
            "Le service d'authentification ne peut pas être null.");
        _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Le logger ne peut pas être null.");
        _emailAuthService = emailAuthService;
        _securityTokenService = securityTokenService;
        _legalConsentService = legalConsentService;
    }


    public async Task<AuthResponseDto> ExecuteAsync(RegisterPublicRequestDto publicRequest, string ipAddress,
        string userAgent,string languageCode)
    {
        _logger.LogInformation("Début de l'inscription pour l'email {Email}", publicRequest.Email);

        // Valide l'email et le mot de passe fournis
        try
        {
            CommonValidator.ValidateEmail(publicRequest.Email);
            CommonValidator.ValidatePassword(publicRequest.Password);
            _logger.LogDebug("Validation de l'email et du mot de passe réussie pour {Email}", publicRequest.Email);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Échec de la validation des données d'entrée pour l'email {Email}",
                publicRequest.Email);
            throw;
        }

        // Vérifie si l'email est déjà utilisé dans le système
        if (await _userRepository.ExistsByEmailAsync(publicRequest.Email))
        {
            _logger.LogWarning("Tentative d'inscription avec un email déjà utilisé : {Email}", publicRequest.Email);
            throw new BusinessException(ErrorCodes.EmailAlreadyUsed, "email");
        }


        // Mappe les données du DTO vers une entité utilisateur
        var user = _mapper.Map<Users>(publicRequest);
        user.PasswordHash = _passwordHasher.Hash(publicRequest.Password); // Hache le mot de passe
        user.SuperRoleId =
            await _superRoleRepository.GetSuperRoleIdAsync("Others"); // Assigne un rôle global par défaut

        user.IsVerified = false; // L'utilisateur n'est pas encore vérifié

        // création de l'email token


        AuthResponseDto authResponse;
        await using var transaction = await _unitOfWork.BeginTransactionAsync(); // Démarre une transaction
        try
        {
            // Ajoute l'utilisateur à la base de données
            await _userRepository.AddAsync(user);
            _logger.LogDebug("Utilisateur ajouté à la base de données pour {Email}", user.Email);
            await _unitOfWork.SaveChangesAsync();

         
            user.CreatedBy = user.UserId;
        
         
            await _userRepository.UpdateAsync(user);
            
            // Ajoute une entrée dans la table email_confirmation
            var generatedTokenResult = await _securityTokenService.GenerateAndStoreAsync(
                user.UserId,
                TokenTypeName,
                ipAddress,
                userAgent
            );

            _logger.LogDebug("Token de confirmation email généré pour {Email}", publicRequest.Email);

            // Récupère le rôle spécifié dans la requête
            var role = await _roleRepository.GetByIdAsync(publicRequest.RoleId)
                       ?? throw new BusinessException(ErrorCodes.RoleNotFound, "RoleId");

            _logger.LogDebug("Rôle {RoleName} récupéré pour l'ID {RoleId}", role.Name, publicRequest.RoleId);

            // Crée l'entité associée (Student ou PrivateLandlord) et met à jour l'utilisateur
            user = await HandleRoleSpecificEntityAsync(user, role, publicRequest);
            _logger.LogDebug("Entité spécifique créée et associée pour l'utilisateur {UserId}", user.UserId);

            // Effectue une connexion automatique avec génération des tokens
            authResponse = await _authService.PerformAutoLoginAsync(
                user,
                ipAddress, // Utilise une adresse IP par défaut si non fournie
                "Inscription et connexion réussies. Veuillez vérifier votre email pour confirmer votre compte.",
                userAgent
            );
            _logger.LogDebug("Connexion automatique effectuée pour l'utilisateur {UserId}", user.UserId);
            
            if (generatedTokenResult.RawToken is null)
                throw new BusinessException(ErrorCodes.TokenGenerationFailed, "token");

            if (generatedTokenResult.CodeOtp != null)
                await _emailAuthService.SendConfirmationEmailLinkAsync(user, generatedTokenResult.RawToken,
                    generatedTokenResult.CodeOtp, languageCode);
            _logger.LogInformation("Email de confirmation envoyé à {Email}", user.Email);

            var userAgentId = await _authService.ProcessUserAgentAsync(userAgent);
            await _legalConsentService.RegisterPrivacyPolicyConsentAsync(user, ipAddress, userAgentId,publicRequest.LegalConsents);
            
            // Persiste toutes les modifications dans la base de données
            await _unitOfWork.SaveChangesAsync();
            await transaction.CommitAsync(); // Valide la transaction
            _logger.LogInformation("Inscription réussie et transaction validée pour l'utilisateur {UserId}",
                user.UserId);

        }
        catch (BusinessException)
        {
            throw;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "Échec de l'inscription pour l'email {Email}, rollback effectué", publicRequest.Email);
            throw new BusinessException(ErrorCodes.UserRegistrationFailed);
        }


        return authResponse;
    }



    private async Task<Users> HandleRoleSpecificEntityAsync(Users user, Role role,
        RegisterPublicRequestDto publicRequest)
    {
        int entityId;

        // Récupère le type d'entité "Public" pour les entités polymorphiques
        var publicEntityType = await _entityTypeRepository.GetByNameAsync("Public");
        if (publicEntityType == null)
        {
            _logger.LogError("Type d'entité 'Public' non trouvé pour l'utilisateur {UserId}", user.UserId);
            throw new BusinessException(ErrorCodes.PublicEntityTypeNotFound);
        }


        var entityTypeId = publicEntityType.EntityTypeId;
        _logger.LogDebug("Type d'entité 'Public' récupéré avec ID {EntityTypeId}", entityTypeId);

        // Crée l'entité spécifique en fonction du rôle
        switch (role.Name)
        {
            case "Student":
                // Crée une entité polymorphique pour l'étudiant
                var studentEntity = new Entity
                {
                    EntityTypeId = entityTypeId,
                    Name = $"{user.FirstName} {user.LastName}",
                    CreatedBy = user.UserId,
                };
                await _entityRepository.AddAsync(studentEntity);
                _logger.LogDebug("Entité polymorphique créée pour l'étudiant avec ID {EntityId}",
                    studentEntity.EntityId);

                // Mappe les données de la requête vers une entité Student
                var student = _mapper.Map<Student>(publicRequest);
                student.EntityId = studentEntity.EntityId;
                await _studentRepository.AddAsync(student);
                _logger.LogDebug("Entité Student créée avec ID {StudentId}", student.StudentId);

                // Met à jour l'entité polymorphique avec l'ID spécifique de l'étudiant
                studentEntity.SpecificEntityId = student.StudentId;
                entityId = studentEntity.EntityId;
                break;

            case "PrivateLandlord":
                // Crée une entité polymorphique pour le bailleur privé
                var landlordEntity = new Entity
                {
                    EntityTypeId = entityTypeId,
                    Name = $"{user.FirstName} {user.LastName}",
                    CreatedBy = user.UserId,
                };
                await _entityRepository.AddAsync(landlordEntity);
                _logger.LogDebug("Entité polymorphique créée pour le bailleur avec ID {EntityId}",
                    landlordEntity.EntityId);

                // Mappe les données de la requête vers une entité Landlord
                var landlord = _mapper.Map<Landlord>(publicRequest);
                landlord.EntityId = landlordEntity.EntityId;
                await _landlordRepository.AddAsync(landlord);
                _logger.LogDebug("Entité Landlord créée avec ID {LandlordId}", landlord.LandlordId);

                // Met à jour l'entité polymorphique avec l'ID spécifique du bailleur
                landlordEntity.SpecificEntityId = landlord.LandlordId;
                entityId = landlordEntity.EntityId;
                break;

            default:
                // Lève une exception si le rôle n'est pas supporté
                _logger.LogError("Rôle non supporté '{RoleName}' pour l'utilisateur {UserId}", role.Name, user.UserId);
                throw new BusinessException(ErrorCodes.UnsupportedPublicRole, "RoleId");
        }

        // Crée une association entre l'utilisateur et l'entité via EntityUser
        var entityUser = _mapper.Map<EntityUser>(publicRequest);
        entityUser.UserId = user.UserId;
        entityUser.EntityId = entityId;
        entityUser.RoleId = publicRequest.RoleId;
        await _entityUserRepository.AddAsync(entityUser);
        _logger.LogDebug("Association EntityUser créée pour UserId {UserId} et EntityId {EntityId}", user.UserId,
            entityId);

        // Recharge l'utilisateur avec toutes ses relations (SuperRole, EntityUser, etc.)
        var updatedUser = await _userRepository.GetByEmailAsync(user.Email)
                          ?? throw new BusinessException(ErrorCodes.UserNotFoundAfterEntityCreation);

        _logger.LogDebug("Utilisateur rechargé avec succès pour l'email {Email}", user.Email);
        return updatedUser;
    }
    
   

}