
using Application.SharedService;
using Application.Validation;
using Application.UseCases.Invitation.Service;
using Application.UseCases.Invitation.SuperAdmin.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Invitation.SuperAdmin.UseCases;


public class AddCompanyUseCase : AddEntityUseCaseBase<CreateCompanyDto, Company, CompanyDto>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly ILogger<AddCompanyUseCase> _logger;

    public AddCompanyUseCase(
        ICompanyRepository companyRepository,
        IInvitationService invitationService,
        IInvitationEmailService invitationEmailService, 
        IRoleRepository roleRepository,
        IEntityTypeRepository entityTypeRepository,
        IMapper mapper,
        IEntityRepository entityRepository,
        ILogger<AddCompanyUseCase> logger,
        IInvitationTypeRepository invitationTypeRepository,
        ISharedUniquenessValidationService sharedUniquenessValidationService)
        : base(invitationService, invitationEmailService, roleRepository, entityTypeRepository, entityRepository,
            mapper, logger, invitationTypeRepository, sharedUniquenessValidationService)
    {
        _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    

 

    protected override async Task ValidateDataAsync(CreateCompanyDto dto)
    {
        _logger.LogDebug("Validation spécifique pour l'entreprise : {CompanyName}", dto.Name);

        // Valider le format du CompanyNumber
        if (!CommonFormatValidator.ValidateCompanyNumber(dto.CompanyNumber))
        {
            _logger.LogWarning("Le numéro BCE '{CompanyNumber}' est invalide.", dto.CompanyNumber);
            throw new BusinessException(ErrorCodes.InvalidCompanyNumberFormat, "companyNumber");
        }

        // Valider l'unicité du CompanyNumber
        await UniquenessValidator.ValidateUniqueCompanyNumberAsync(dto.CompanyNumber);

        _logger.LogDebug("Validation spécifique terminée pour {CompanyName}", dto.Name);
    }

    protected override async Task<Company> CreateSpecificEntityAsync(CreateCompanyDto dto, Entity entity, int createdBy)
    {
        _logger.LogInformation("Création d'une entreprise spécifique : {CompanyName}", dto.Name);

        var company = new Company
        {
            EntityId = entity.EntityId,
            Name = dto.Name,
            CompanyNumber = dto.CompanyNumber,
            CreatedBy = createdBy,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        try
        {
            await _companyRepository.AddAsync(company);
            _logger.LogDebug("Entreprise {CompanyName} ajoutée avec succès, ID : {CompanyId}", company.Name,
                company.CompanyId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Échec de l'ajout de l'entreprise {CompanyName} à la base de données.", dto.Name);
            throw new BusinessException(ErrorCodes.CompanyCreationFailed);
        }

        return company;
    }

    protected override string GetEntityTypeName()
    {
        return "Company";
    }

    protected override string GetInvitationEmail(CreateCompanyDto dto)
    {
        _logger.LogDebug("Récupération de l'email d'invitation : {Email}", dto.InvitationEmail);
        return dto.InvitationEmail;
    }

    protected override int GetEntityId(Company entity)
    {
        _logger.LogDebug("Récupération de l'ID de l'entreprise : {CompanyId}", entity.CompanyId);
        return entity.CompanyId;
    }

    protected override CompanyDto MapToDto(Company entity, Role role)
    {
        _logger.LogDebug("Mappage de l'entreprise {CompanyId} et du rôle {RoleId} en DTO.", entity.CompanyId,
            role.RoleId);

        var dto = Mapper.Map<CompanyDto>(entity);
        dto.Role = Mapper.Map<RoleDto>(role);

        _logger.LogDebug("Mappage terminé pour l'entreprise {CompanyId}", entity.CompanyId);
        return dto;
    }

    public override async Task<CompanyDto> ExecuteAsync(CreateCompanyDto dto, int createdBy,string languageCode)
    {
        _logger.LogInformation("Début de l'ajout de l'entreprise {CompanyName} par l'utilisateur {CreatedBy}", dto.Name,
            createdBy);

        try
        {
            var result = await base.ExecuteAsync(dto, createdBy, languageCode);
            _logger.LogInformation("Entreprise {CompanyName} ajoutée avec succès par l'utilisateur {CreatedBy}",
                dto.Name, createdBy);
            return result;
        }
        catch (BusinessException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Échec de l'ajout de l'entreprise {CompanyName} par l'utilisateur {CreatedBy}",
                dto.Name, createdBy);
            throw new BusinessException(ErrorCodes.CompanyCreationFailed);
        }
    }
}