
using Application.Validation;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Invitation.Service;

public class InvitationService : IInvitationService
{
    private readonly ISuperadminInvitationEntityRepository _entityRepository;
    private readonly IEntityTypeRepository _entityTypeRepository;
    private readonly ISuperadminInvitationRepository _invitationRepository;
    private readonly ILogger<InvitationService> _logger;
    private readonly IRoleRepository _roleRepository;
    private readonly ITokenHasher _tokenHasher;
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;

    public InvitationService(
        ISuperadminInvitationRepository invitationRepository,
        ISuperadminInvitationEntityRepository entityRepository,
        IEntityTypeRepository entityTypeRepository,
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        ITokenService tokenService,
        ITokenHasher tokenHasher,
        ILogger<InvitationService> logger)
    {
        _invitationRepository = invitationRepository ?? throw new ArgumentNullException(nameof(invitationRepository), "Le référentiel d'invitations de superadmin ne peut pas être null.");
        _entityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository), "Le référentiel d'entités de superadmin ne peut pas être null.");
        _entityTypeRepository = entityTypeRepository ?? throw new ArgumentNullException(nameof(entityTypeRepository), "Le référentiel de types d'entité ne peut pas être null.");
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository), "Le référentiel utilisateur ne peut pas être null.");
        _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository), "Le référentiel de rôles ne peut pas être null.");
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService), "Le service de jetons ne peut pas être null.");
        _tokenHasher = tokenHasher ?? throw new ArgumentNullException(nameof(tokenHasher), "Le service de hachage de jetons ne peut pas être null.");
        _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Le logger ne peut pas être null.");
    }

    public async Task<(SuperadminInvitation invitation, EntityType entityType, string rawToken)> CreateInvitationAsync(
        string email, int entityId, string entityTypeName, int roleId, int? createdBy, int invitationTypeId)
    {
        _logger.LogInformation("Creating invitation for {Email}", email);
        await ValidateEmailAsync(email);

        var (invitation, rawToken) = await CreateBaseInvitationAsync(email, roleId, createdBy, invitationTypeId);
        var entityType = await ValidateAndGetEntityTypeAsync(entityTypeName);
        await LinkInvitationToEntityAsync(invitation, entityId, roleId);

        return (invitation, entityType, rawToken);
    }

    public async Task<(SuperadminInvitation invitation, string rawToken)> CreateColleagueInvitationAsync(
        string email, int entityId, int roleId, int invitingUserId, int invitationTypeId)
    {
        _logger.LogInformation("Creating colleague invitation for {Email} by {InvitingUserId}", email, invitingUserId);
        await ValidateEmailAsync(email);
        var entityTypeId = await ValidateInvitingUserAsync(invitingUserId, roleId);

        var (invitation, rawToken) = await CreateBaseInvitationAsync(email, roleId, invitingUserId, invitationTypeId);
        await LinkInvitationToEntityAsync(invitation, entityId, roleId);

        return (invitation, rawToken);
    }

    private async Task<(SuperadminInvitation invitation, string rawToken)> CreateBaseInvitationAsync(
        string email, int roleId, int? createdBy, int invitationTypeId)
    {
        var rawToken = _tokenService.GenerateEmailConfirmInviteToken();
        var hashedToken = _tokenHasher.HashToken(rawToken);
        _logger.LogDebug("Generated raw token: {RawToken}, hashed: {HashedToken}", rawToken, hashedToken);

        var invitation = new SuperadminInvitation
        {
            Email = email,
            InvitationToken = hashedToken,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            Used = false,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = createdBy,
            InvitationTypeId = invitationTypeId
        };

        await _invitationRepository.AddAsync(invitation);
        _logger.LogDebug("Invitation saved with ID: {InvitationId}", invitation.SuperadminInvitationId);

        return (invitation, rawToken);
    }

    private async Task LinkInvitationToEntityAsync(SuperadminInvitation invitation, int entityId, int roleId)
    {
        var invitationEntity = new SuperadminInvitationEntity
        {
            SuperadminInvitationId = invitation.SuperadminInvitationId,
            EntityId = entityId,
            RoleId = roleId,
            CreatedAt = DateTime.UtcNow
        };

        await _entityRepository.AddAsync(invitationEntity);
        _logger.LogDebug("Invitation linked to entity {EntityId} with role {RoleId}", entityId, roleId);
    }

    private async Task ValidateEmailAsync(string email)
    {
        _logger.LogDebug("Validating email: {Email}", email);
        try
        {
            CommonFormatValidator.ValidateEmail(email);
        }
        catch (ArgumentException)
        {
            _logger.LogWarning("Invalid email format: {Email}", email);
            throw;
        }

        if (await _userRepository.ExistsByEmailAsync(email.ToLower()) ||
            await _invitationRepository.ExistsByEmailAsync(email.ToLower()))
        {
            _logger.LogWarning("Email {Email} already used or has pending invitation", email);
            throw new BusinessException(ErrorCodes.EmailAlreadyUsed, "email");
        }
    }

    private async Task<EntityType> ValidateAndGetEntityTypeAsync(string entityTypeName)
    {
        var entityType = await _entityTypeRepository.GetByNameAsync(entityTypeName);
        if (entityType == null)
        {
            _logger.LogError("Entity type '{EntityTypeName}' not found", entityTypeName);
            throw new BusinessException(ErrorCodes.EntityTypeNotFound, "entityTypeName");
        }

        return entityType;
    }

    private async Task<int> ValidateInvitingUserAsync(int invitingUserId, int roleId)
    {
        var invitingUser = await _userRepository.GetByIdAsync(invitingUserId);
        if (invitingUser == null || invitingUser.EntityUser == null)
        {
            _logger.LogWarning("Inviting user {InvitingUserId} not found or not linked to entity", invitingUserId);
            throw new BusinessException(ErrorCodes.InviterNotLinkedToEntity, "invitingUserId");
        }

        if (invitingUser.EntityUser.Role.Name != "Admin")
        {
            _logger.LogWarning("User {InvitingUserId} is not an Admin", invitingUserId);
            throw new BusinessException(ErrorCodes.InviterNotAdmin, "invitingUserId");
        }

        var entityTypeId = invitingUser.EntityUser.Entity.EntityTypeId;
        var validRoles = await _roleRepository.GetRolesByEntityTypeIdAsync(entityTypeId);
        if (validRoles == null || !validRoles.Any(r => r.RoleId == roleId))
        {
            _logger.LogWarning("Role {RoleId} invalid for entity type {EntityTypeId}", roleId, entityTypeId);
            throw new BusinessException(ErrorCodes.RoleNotValidForEntityType, "RoleId");
        }

        return entityTypeId;
    }
}