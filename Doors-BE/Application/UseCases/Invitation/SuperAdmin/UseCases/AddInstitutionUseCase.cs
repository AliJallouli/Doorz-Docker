
using Application.Validation;
using Application.UseCases.Invitation.Service;
using Application.UseCases.Invitation.SuperAdmin.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Invitation.SuperAdmin.UseCases;


public class AddInstitutionUseCase : AddEntityUseCaseBase<CreateInstitutionDto, Institution, InstitutionDto>
{
    private readonly IInstitutionRepository _institutionRepository;
    private readonly IInstitutionTypeRepository _institutionTypeRepository;
    private readonly ILogger<AddInstitutionUseCase> _logger;

    public AddInstitutionUseCase(
        IInstitutionRepository institutionRepository,
        IInstitutionTypeRepository institutionTypeRepository,
        IInvitationService invitationService, // Remplacé IUnifiedInvitationService
        IInvitationEmailService invitationEmailService, // Ajouté pour correspondre à AddEntityUseCaseBase
        IRoleRepository roleRepository,
        IEntityTypeRepository entityTypeRepository,
        IMapper mapper,
        IEntityRepository entityRepository,
        ILogger<AddInstitutionUseCase> logger,
        IInvitationTypeRepository invitationTypeRepository)
        : base(invitationService, invitationEmailService, roleRepository, entityTypeRepository, entityRepository,
            mapper, logger, invitationTypeRepository)
    {
        _institutionRepository =
            institutionRepository ?? throw new ArgumentNullException(nameof(institutionRepository));
        _institutionTypeRepository = institutionTypeRepository ??
                                     throw new ArgumentNullException(nameof(institutionTypeRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    protected override Task ValidateInputAsync(CreateInstitutionDto dto)
    {
        _logger.LogDebug("Validation des données d'entrée pour la création de l'institution : {InstitutionName}",
            dto.Name);

        try
        {
            InstitutionValidator.Validate(dto);
            _logger.LogDebug("Données d'entrée validées avec succès pour {InstitutionName}", dto.Name);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Échec de la validation des données d'entrée pour {InstitutionName} : {Message}",
                dto.Name, ex.Message);
            throw new BusinessException(ErrorCodes.InvalidInstitutionInput, ex.ParamName ?? "institution");
        }

        return Task.CompletedTask;
    }

    protected override async Task ValidateUniqueNameAsync(CreateInstitutionDto dto)
    {
        _logger.LogDebug("Vérification de l'unicité du nom de l'institution : {InstitutionName}", dto.Name);

        if (await _institutionRepository.ExistNameAsync(dto.Name))
        {
            _logger.LogWarning("Le nom de l'institution {InstitutionName} existe déjà.", dto.Name);
            throw new BusinessException(ErrorCodes.InstitutionNameAlreadyUsed, "name");
        }

        _logger.LogDebug("Nom de l'institution {InstitutionName} confirmé comme unique.", dto.Name);
    }

    protected override async Task ValidateDataAsync(CreateInstitutionDto dto)
    {
        _logger.LogDebug("Validation des données supplémentaires pour {InstitutionName}", dto.Name);

        if (!await _institutionTypeRepository.ExistsByIdAsync(dto.InstitutionTypeId))
        {
            _logger.LogWarning("Le type d'institution {InstitutionTypeId} n'existe pas pour {InstitutionName}.",
                dto.InstitutionTypeId, dto.Name);
            throw new BusinessException(ErrorCodes.InstitutionTypeNotFound, "institutionTypeId");
        }

        _logger.LogDebug("Type d'institution {InstitutionTypeId} validé pour {InstitutionName}", dto.InstitutionTypeId,
            dto.Name);
    }

    protected override async Task<Institution> CreateSpecificEntityAsync(CreateInstitutionDto dto, Entity entity,
        int createdBy)
    {
        _logger.LogInformation("Création d'une institution spécifique : {InstitutionName}", dto.Name);

        var institution = new Institution
        {
            EntityId = entity.EntityId,
            Name = dto.Name,
            InstitutionTypeId = dto.InstitutionTypeId,
            CreatedBy = createdBy
        };

        try
        {
            await _institutionRepository.AddAsync(institution);
            _logger.LogDebug("Institution {InstitutionName} ajoutée avec succès, ID : {InstitutionId}",
                institution.Name, institution.InstitutionId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Échec de l'ajout de l'institution {InstitutionName} à la base de données.", dto.Name);
            throw new BusinessException(ErrorCodes.InstitutionCreationFailed);
        }

        return institution;
    }

    protected override string GetEntityTypeName()
    {
        return "Institution";
    }

    protected override string GetInvitationEmail(CreateInstitutionDto dto)
    {
        _logger.LogDebug("Récupération de l'email d'invitation : {Email}", dto.InvitationEmail);
        return dto.InvitationEmail;
    }

    protected override int GetEntityId(Institution entity)
    {
        _logger.LogDebug("Récupération de l'ID de l'institution : {InstitutionId}", entity.InstitutionId);
        return entity.InstitutionId;
    }

    protected override InstitutionDto MapToDto(Institution entity, Role role)
    {
        _logger.LogDebug("Mappage de l'institution {InstitutionId} et du rôle {RoleId} en DTO.", entity.InstitutionId,
            role.RoleId);

        var dto = Mapper.Map<InstitutionDto>(entity);
        dto.Role = Mapper.Map<RoleDto>(role);

        _logger.LogDebug("Mappage terminé pour l'institution {InstitutionId}", entity.InstitutionId);
        return dto;
    }

    public override async Task<InstitutionDto> ExecuteAsync(CreateInstitutionDto dto, int createdBy,string languageCode)
    {
        _logger.LogInformation("Début de l'ajout de l'institution {InstitutionName} par l'utilisateur {CreatedBy}",
            dto.Name, createdBy);

        try
        {
            var result = await base.ExecuteAsync(dto, createdBy,languageCode);
            _logger.LogInformation("Institution {InstitutionName} ajoutée avec succès par {CreatedBy}", dto.Name,
                createdBy);
            return result;
        }
        catch (BusinessException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Échec de l'ajout de l'institution {InstitutionName} par {CreatedBy}", dto.Name,
                createdBy);
            throw new BusinessException(ErrorCodes.InstitutionCreationFailed);
        }
    }
}