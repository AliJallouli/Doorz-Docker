using Application.SharedService;
using Application.UseCases.Invitation.Service;
using Application.UseCases.Invitation.SuperAdmin.DTOs;
using Application.Validation;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Invitation.SuperAdmin.UseCases;

public class AddAssociationUseCase: AddEntityUseCaseBase<CreateAssociationDto, Association, AssociationDto>
{
    private readonly IAssociationRepository _associationRepository;
    private readonly ILogger<AddAssociationUseCase> _logger;
    
    public AddAssociationUseCase(
        IAssociationRepository associationRepository,
        IInvitationService invitationService,
        IInvitationEmailService emailService, 
        IRoleRepository roleRepository, 
        IEntityTypeRepository entityTypeRepository, 
        IEntityRepository entityRepository,
        IMapper mapper, 
        ILogger<AddAssociationUseCase> logger,
        IInvitationTypeRepository invitationTypeRepository,  ISharedUniquenessValidationService sharedUniquenessValidationService)
        : base(invitationService, emailService, roleRepository, entityTypeRepository, entityRepository, mapper, logger, invitationTypeRepository, sharedUniquenessValidationService)
    {
        _associationRepository = associationRepository ?? throw new ArgumentNullException(nameof(associationRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    
    protected override Task ValidateDataAsync(CreateAssociationDto dto)
    {
        _logger.LogDebug("Validation des données supplémentaires pour {AssociationName} (aucune validation spécifique).",
            dto.Name);
        return Task.CompletedTask;
    }

    protected override async Task<Association> CreateSpecificEntityAsync(CreateAssociationDto dto, Entity entity, int createdBy)
    {
        _logger.LogInformation("Création d'une association spécifique : {AssociationName}", dto.Name);

        var association = new Association
        {
            EntityId = entity.EntityId,
            Name = dto.Name,
            CreatedBy = createdBy,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        try
        {
            await _associationRepository.AddAsync(association);
            _logger.LogDebug("Entreprise {AssociationName} ajoutée avec succès, ID : {AssociationId}", association.Name,
                association.AssociationId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Échec de l'ajout de l'association {AssociationName} à la base de données.", dto.Name);
            throw new BusinessException(ErrorCodes.AssociationCreationFailed);
        }

        return association;
    }

    protected override string GetEntityTypeName()
    {
        return "Association";
    }

    protected override string GetInvitationEmail(CreateAssociationDto dto)
    {
        _logger.LogDebug("Récupération de l'email d'invitation : {Email}", dto.InvitationEmail);
        return dto.InvitationEmail;
    }

    protected override int GetEntityId(Association entity)
    {
        _logger.LogDebug("Récupération de l'ID de l'association : {AssociationId}", entity.AssociationId);
        return entity.AssociationId;
    }

    protected override AssociationDto MapToDto(Association entity, Role role)
    {
        _logger.LogDebug("Mappage de l'association {AssociationId} et du rôle {RoleId} en DTO.", entity.AssociationId,
            role.RoleId);

        var dto = Mapper.Map<AssociationDto>(entity);
        dto.Role = Mapper.Map<RoleDto>(role);

        _logger.LogDebug("Mappage terminé pour l'associatrion {AssociationId}", entity.AssociationId);
        return dto;
    }
    
    public override async Task<AssociationDto> ExecuteAsync(CreateAssociationDto dto, int createdBy,string languageCode)
    {
        _logger.LogInformation("Début de l'ajout de l'association {AssociationName} par l'utilisateur {CreatedBy}", dto.Name,
            createdBy);

        try
        {
            var result = await base.ExecuteAsync(dto, createdBy, languageCode);
            _logger.LogInformation("Association {AssociationName} ajoutée avec succès par l'utilisateur {CreatedBy}",
                dto.Name, createdBy);
            return result;
        }
        catch (BusinessException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Échec de l'ajout de l'association {AssociationName} par l'utilisateur {CreatedBy}",
                dto.Name, createdBy);
            throw new BusinessException(ErrorCodes.AssociationCreationFailed);
        }
    }
}