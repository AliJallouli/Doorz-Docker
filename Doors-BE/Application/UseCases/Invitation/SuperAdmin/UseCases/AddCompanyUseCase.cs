
using Application.Validation;
using Application.UseCases.Invitation.Service;
using Application.UseCases.Invitation.SuperAdmin.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
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
        IInvitationTypeRepository invitationTypeRepository)
        : base(invitationService, invitationEmailService, roleRepository, entityTypeRepository, entityRepository,
            mapper, logger, invitationTypeRepository)
    {
        _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    protected override Task ValidateInputAsync(CreateCompanyDto dto)
    {
        _logger.LogDebug("Validation des données d'entrée pour la création de l'entreprise : {CompanyName}", dto.Name);

        try
        {
            CompanyValidator.Validate(dto);
            _logger.LogDebug("Données d'entrée validées avec succès pour {CompanyName}", dto.Name);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Échec de la validation des données d'entrée pour {CompanyName} : {Message}", dto.Name,
                ex.Message);
            throw new BusinessException(ErrorCodes.InvalidCompanyInput, ex.ParamName ?? "company");
        }

        return Task.CompletedTask;
    }

    protected override async Task ValidateUniqueNameAsync(CreateCompanyDto dto)
    {
        _logger.LogDebug("Vérification de l'unicité du nom de l'entreprise : {CompanyName}", dto.Name);

        if (await _companyRepository.ExistNameAsync(dto.Name))
        {
            _logger.LogWarning("Le nom de l'entreprise {CompanyName} existe déjà.", dto.Name);
            throw new BusinessException(ErrorCodes.CaCompanyNameAlreadyUsed, "name");
        }

        _logger.LogDebug("Nom de l'entreprise {CompanyName} confirmé comme unique.", dto.Name);
    }

    protected override Task ValidateDataAsync(CreateCompanyDto dto)
    {
        _logger.LogDebug("Validation des données supplémentaires pour {CompanyName} (aucune validation spécifique).",
            dto.Name);
        return Task.CompletedTask;
    }

    protected override async Task<Company> CreateSpecificEntityAsync(CreateCompanyDto dto, Entity entity, int createdBy)
    {
        _logger.LogInformation("Création d'une entreprise spécifique : {CompanyName}", dto.Name);

        var company = new Company
        {
            EntityId = entity.EntityId,
            Name = dto.Name,
            CompanyNumber = dto.CompanyNumber,
            CreatedBy = createdBy
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