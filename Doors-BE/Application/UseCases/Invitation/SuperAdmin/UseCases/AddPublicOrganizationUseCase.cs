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

public class AddPublicOrganizationUseCase:AddEntityUseCaseBase<CreatePublicOrganizationDto, PublicOrganization, PublicOrganizationDto>
{
      private readonly IPublicOrganizationRepository _publicOrganizationRepository;
    private readonly ILogger<AddPublicOrganizationUseCase> _logger;

    public AddPublicOrganizationUseCase(
        IPublicOrganizationRepository publicOrganizationRepository,
        IInvitationService invitationService,
        IInvitationEmailService emailService,
        IRoleRepository roleRepository,
        IEntityTypeRepository entityTypeRepository,
        IEntityRepository entityRepository,
        IMapper mapper,
        ILogger<AddPublicOrganizationUseCase> logger,
        IInvitationTypeRepository invitationTypeRepository,
        ISharedUniquenessValidationService sharedUniquenessValidationService)
        : base(invitationService, emailService, roleRepository, entityTypeRepository, entityRepository, mapper, logger, invitationTypeRepository, sharedUniquenessValidationService)
    {
        _publicOrganizationRepository = publicOrganizationRepository ?? throw new ArgumentNullException(nameof(publicOrganizationRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }



  

    protected override Task ValidateDataAsync(CreatePublicOrganizationDto dto)
    {
        _logger.LogDebug("Validation des données supplémentaires pour {PublicOrganizationName} (aucune validation spécifique).", dto.Name);
        return Task.CompletedTask;
    }

    protected override async Task<PublicOrganization> CreateSpecificEntityAsync(CreatePublicOrganizationDto dto, Entity entity, int createdBy)
    {
        _logger.LogInformation("Création d’un organisme public spécifique : {PublicOrganizationName}", dto.Name);

        var publicOrganization = new PublicOrganization
        {
            EntityId = entity.EntityId,
            Name = dto.Name,
            CreatedBy = createdBy,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        try
        {
            await _publicOrganizationRepository.AddAsync(publicOrganization);
            _logger.LogDebug("Organisme public {PublicOrganizationName} ajouté avec succès, ID : {PublicOrganizationId}", publicOrganization.Name, publicOrganization.PublicOrganizationId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Échec de l'ajout de l’organisme public {PublicOrganizationName} à la base de données.", dto.Name);
            throw new BusinessException(ErrorCodes.PublicOrganizationCreationFailed);
        }

        return publicOrganization;
    }

    protected override string GetEntityTypeName()
    {
        return "PublicOrganization";
    }

    protected override string GetInvitationEmail(CreatePublicOrganizationDto dto)
    {
        _logger.LogDebug("Récupération de l'email d'invitation : {Email}", dto.InvitationEmail);
        return dto.InvitationEmail;
    }

    protected override int GetEntityId(PublicOrganization entity)
    {
        _logger.LogDebug("Récupération de l'ID de l’organisme public : {PublicOrganizationId}", entity.PublicOrganizationId);
        return entity.PublicOrganizationId;
    }

    protected override PublicOrganizationDto MapToDto(PublicOrganization entity, Role role)
    {
        _logger.LogDebug("Mappage de l’organisme public {PublicOrganizationId} et du rôle {RoleId} en DTO.", entity.PublicOrganizationId, role.RoleId);

        var dto = Mapper.Map<PublicOrganizationDto>(entity);
        dto.Role = Mapper.Map<RoleDto>(role);

        _logger.LogDebug("Mappage terminé pour l’organisme public {PublicOrganizationId}", entity.PublicOrganizationId);
        return dto;
    }

    public override async Task<PublicOrganizationDto> ExecuteAsync(CreatePublicOrganizationDto dto, int createdBy, string languageCode)
    {
        _logger.LogInformation("Début de l'ajout de l’organisme public {PublicOrganizationName} par l'utilisateur {CreatedBy}", dto.Name, createdBy);

        try
        {
            var result = await base.ExecuteAsync(dto, createdBy, languageCode);
            _logger.LogInformation("Organisme public {PublicOrganizationName} ajouté avec succès par l'utilisateur {CreatedBy}", dto.Name, createdBy);
            return result;
        }
        catch (BusinessException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Échec de l'ajout de l’organisme public {PublicOrganizationName} par l'utilisateur {CreatedBy}", dto.Name, createdBy);
            throw new BusinessException(ErrorCodes.PublicOrganizationCreationFailed);
        }
    }
}