using System.Transactions;
using Application.SharedService;
using Application.UseCases.Invitation.Service;
using Application.Validation;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Invitation.SuperAdmin.UseCases;

public abstract class AddEntityUseCaseBase<TCreateDto, TEntity, TDto> : IAddEntityUseCase<TCreateDto, TDto>
    where TCreateDto : class
    where TEntity : class
    where TDto : class
{
    protected readonly IEntityRepository EntityRepository;
    protected readonly IEntityTypeRepository EntityTypeRepository;
    protected readonly IInvitationEmailService InvitationEmailService;
    protected readonly IInvitationService InvitationService;
    protected readonly IInvitationTypeRepository InvitationTypeRepository;
    protected readonly ILogger<AddEntityUseCaseBase<TCreateDto, TEntity, TDto>> Logger;
    protected readonly IMapper Mapper;
    protected readonly IRoleRepository RoleRepository;
    protected readonly ISharedUniquenessValidationService UniquenessValidator;

    protected AddEntityUseCaseBase(
        IInvitationService invitationService,
        IInvitationEmailService emailService,
        IRoleRepository roleRepository,
        IEntityTypeRepository entityTypeRepository,
        IEntityRepository entityRepository,
        IMapper mapper,
        ILogger<AddEntityUseCaseBase<TCreateDto, TEntity, TDto>> logger,
        IInvitationTypeRepository invitationTypeRepository,
        ISharedUniquenessValidationService uniquenessValidator)
    {
        InvitationService = invitationService ?? throw new ArgumentNullException(nameof(invitationService), "Le service d'invitation ne peut pas être null.");
        InvitationEmailService = emailService ?? throw new ArgumentNullException(nameof(emailService), "Le service d'email d'invitation ne peut pas être null.");
        RoleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository), "Le référentiel de rôles ne peut pas être null.");
        EntityTypeRepository = entityTypeRepository ?? throw new ArgumentNullException(nameof(entityTypeRepository), "Le référentiel de types d'entité ne peut pas être null.");
        EntityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository), "Le référentiel d'entité ne peut pas être null.");
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), "Le mapper ne peut pas être null.");
        Logger = logger ?? throw new ArgumentNullException(nameof(logger), "Le logger ne peut pas être null.");
        InvitationTypeRepository = invitationTypeRepository ?? throw new ArgumentNullException(nameof(invitationTypeRepository), "Le référentiel de types d'invitation ne peut pas être null.");
        UniquenessValidator = uniquenessValidator ?? throw new ArgumentNullException(nameof(uniquenessValidator), "Le validateur d'unicité ne peut pas être null.");
    }

    public virtual async Task<TDto> ExecuteAsync(TCreateDto dto, int createdBy,string languageCode)
    {
        Logger.LogInformation("Starting entity creation of type {EntityType} by {CreatedBy}", typeof(TEntity).Name,
            createdBy);

        try
        {
            await ValidateInputAsync(dto);
            await ValidateUniqueNameAsync(dto);
            await ValidateDataAsync(dto);

            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var (entity, specificEntity) = await CreateAndLinkEntitiesAsync(dto, createdBy);

            var entityType = await GetEntityTypeAsync(GetEntityTypeName());
            var role = await GetAdminRoleAsync(entityType.EntityTypeId);
            var invitationTypeId = await InvitationTypeRepository.GetIdByNameAsync("EntityAdmin");

            var (_, _, rawToken) = await InvitationService.CreateInvitationAsync(
                GetInvitationEmail(dto), entity.EntityId, GetEntityTypeName(), role.RoleId, createdBy,
                invitationTypeId);


            await InvitationEmailService.SendInvitationEmailAsync(
                GetInvitationEmail(dto),
                rawToken,
                entity.Name,
                GetEntityTypeName(),
                role.Name,
                "InvitationEmailTemplate",
                false,languageCode);


            transaction.Complete();
            return MapToDto(specificEntity, role);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Failed to add entity of type {EntityType} by {CreatedBy}", typeof(TEntity).Name,
                createdBy);
            throw;
        }
    }

    protected virtual async Task ValidateInputAsync(TCreateDto dto)
    {
        Logger.LogDebug("Validation des données d'entrée pour l'entité de type {EntityType}", typeof(TEntity).Name);

        // Vérifier que le DTO n'est pas null
        if (!CommonFormatValidator.ValidateNotNull(dto))
        {
            Logger.LogWarning("Le DTO de création est null.");
            throw new BusinessException(ErrorCodes.InvalidInput, "dto");
        }

        // Validation du nom
        var name = GetEntityName(dto);
        if (!CommonFormatValidator.ValidateEntityName(name))
        {
            Logger.LogWarning("Le nom de l'entité '{EntityName}' est invalide.", name);
            throw new BusinessException(ErrorCodes.InvalidEntityNameFormat, "name");
        }
        
        // Validation de l'unicité du nom dans la db
        await UniquenessValidator.ValidateUniqueEntityNameInEntitiesAsync(name);

        // Validation de l'email
        var email = GetInvitationEmail(dto);
        if (!CommonFormatValidator.ValidateEmail(email))
        {
            Logger.LogWarning("L'email '{Email}' est invalide.", email);
            throw new BusinessException(ErrorCodes.InvalidEmailFormat, "invitationEmail");
        }
    
        // Validation de l'unicité de l'email dans superadmininvitation et users
        await UniquenessValidator.ValidateUniqueEmailInSuperAdminInvitationAsync(email,"SuperadminInvitationEmail");
        await UniquenessValidator.ValidateUniqueEmailInUsersAsync(name,"SuperadminInvitationEmail");

        Logger.LogDebug("Validation des données d'entrée terminée pour {EntityName}", name);
    }

    protected virtual Task ValidateUniqueNameAsync(TCreateDto dto)
    {
        var name = GetEntityName(dto);
        return ValidateGlobalEntityNameUniquenessAsync(name);
    }
    
    protected virtual async Task ValidateGlobalEntityNameUniquenessAsync(string name)
    {
        var alreadyExists = await EntityRepository.ExistNameAsync(name);
        if (alreadyExists)
        {
            Logger.LogWarning("Le nom d'entité '{EntityName}' est déjà utilisé.", name);
            throw new BusinessException(ErrorCodes.EntityNameAlreadyUsed, "name");
        }

        Logger.LogDebug("Nom d'entité '{EntityName}' confirmé comme unique.", name);
    }


    protected abstract Task ValidateDataAsync(TCreateDto dto);
    protected abstract Task<TEntity> CreateSpecificEntityAsync(TCreateDto dto, Entity entity, int createdBy);
    protected abstract string GetEntityTypeName();
    protected abstract string GetInvitationEmail(TCreateDto dto);
    protected abstract int GetEntityId(TEntity entity);
    protected abstract TDto MapToDto(TEntity entity, Role role);

    protected virtual async Task<(Entity entity, TEntity specificEntity)> CreateAndLinkEntitiesAsync(TCreateDto dto,
        int createdBy)
    {
        var entityTypeName = GetEntityTypeName();
        var entity = new Entity
        {
            EntityTypeId = (await EntityTypeRepository.GetByNameAsync(entityTypeName)).EntityTypeId,
            Name = GetEntityName(dto),
            CreatedBy = createdBy,
           CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        await EntityRepository.AddAsync(entity);
        var specificEntity = await CreateSpecificEntityAsync(dto, entity, createdBy);
        entity.SpecificEntityId = GetEntityId(specificEntity);
        
        entity.ParentEntityId = entity.EntityId;
        await EntityRepository.UpdateAsync(entity);

        return (entity, specificEntity);
    }

    protected virtual string GetEntityName(TCreateDto dto)
    {
        var name = dto.GetType().GetProperty("Name")?.GetValue(dto)?.ToString();
        if (string.IsNullOrWhiteSpace(name))
            throw new BusinessException(ErrorCodes.EntityNameMissing, "name");
        return name;
    }

    private async Task<EntityType> GetEntityTypeAsync(string entityTypeName)
    {
        var entityType = await EntityTypeRepository.GetByNameAsync(entityTypeName);
        if (entityType == null)
            throw new BusinessException(ErrorCodes.EntityTypeNotFound, "entityTypeName");
        return entityType;
    }

    private async Task<Role> GetAdminRoleAsync(int entityTypeId)
    {
        var role = await RoleRepository.GetByNameAndEntityTypeAsync("Admin", entityTypeId);
        if (role == null)
            throw new BusinessException(ErrorCodes.AdminRoleNotFound, "role");
        return role;
    }
}