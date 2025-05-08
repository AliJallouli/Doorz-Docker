using System.Transactions;
using Application.UseCases.Invitation.Service;
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

    protected AddEntityUseCaseBase(
        IInvitationService invitationService,
        IInvitationEmailService emailService,
        IRoleRepository roleRepository,
        IEntityTypeRepository entityTypeRepository,
        IEntityRepository entityRepository,
        IMapper mapper,
        ILogger<AddEntityUseCaseBase<TCreateDto, TEntity, TDto>> logger,
        IInvitationTypeRepository invitationTypeRepository)
    {
        InvitationService = invitationService ?? throw new ArgumentNullException(nameof(invitationService));
        InvitationEmailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        RoleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        EntityTypeRepository = entityTypeRepository ?? throw new ArgumentNullException(nameof(entityTypeRepository));
        EntityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        InvitationTypeRepository = invitationTypeRepository ??
                                    throw new ArgumentNullException(nameof(invitationTypeRepository));
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

    protected abstract Task ValidateInputAsync(TCreateDto dto);
    protected abstract Task ValidateUniqueNameAsync(TCreateDto dto);
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
            Name = GetEntityName(dto)
        };

        await EntityRepository.AddAsync(entity);
        var specificEntity = await CreateSpecificEntityAsync(dto, entity, createdBy);
        entity.SpecificEntityId = GetEntityId(specificEntity);
        entity.CreatedBy = createdBy;
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