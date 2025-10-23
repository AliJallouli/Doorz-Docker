
using Application.SharedService;
using Application.UseCases.Invitation.Request.DTOs;
using Application.Validation;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Invitation.Request.UseCases;

public class RequestInvitationRequestUseCase
{
    private readonly IInvitationRequestRepository _invitationRequestRepository;
    private readonly IEntityTypeRepository _entityTypeRepository;
    private readonly ISharedUniquenessValidationService _uniquenessValidator;
    private readonly ILogger<RequestInvitationRequestUseCase> _logger;
    private readonly IMapper _mapper;
    private readonly ISpokenLanguageRepository _spokenLanguageRepository;
    private readonly IInstitutionTypeRepository _institutionTypeRepository;

    public RequestInvitationRequestUseCase(
        IInvitationRequestRepository invitationRequestRepository,
        IEntityTypeRepository entityTypeRepository,
        ISharedUniquenessValidationService uniquenessValidator,
        ILogger<RequestInvitationRequestUseCase> logger,
        IMapper mapper,
        ISpokenLanguageRepository spokenLanguageRepository,
        IInstitutionTypeRepository institutionTypeRepository)
    {
        _invitationRequestRepository = invitationRequestRepository ?? throw new ArgumentNullException(nameof(invitationRequestRepository), "Le référentiel de requêtes d'invitation ne peut pas être null.");
        _entityTypeRepository = entityTypeRepository ?? throw new ArgumentNullException(nameof(entityTypeRepository), "Le référentiel de types d'entité ne peut pas être null.");
        _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Le logger ne peut pas être null.");
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), "Le mapper ne peut pas être null.");
        _uniquenessValidator = uniquenessValidator ?? throw new ArgumentNullException(nameof(uniquenessValidator), "Le validateur d'unicité ne peut pas être null.");
        _spokenLanguageRepository = spokenLanguageRepository ?? throw new ArgumentNullException(nameof(spokenLanguageRepository), "Le référentiel de langues parlées ne peut pas être null.");
        _institutionTypeRepository = institutionTypeRepository ?? throw new ArgumentNullException(nameof(institutionTypeRepository), "Le référentiel de types d'institution ne peut pas être null.");
    }

    public async Task ExecuteAsync(CreateInvitationRequestDto dto, string ipAddress, string userAgent, string languageCode)
    {
        _logger.LogInformation("Soumission d'une demande d'invitation pour {EntityName} ({Email})", dto.Name,
            dto.InvitationEmail);

        try
        {
            // Validation des format des données reçu
            if (!InvitationRequestFormatValidator.Validate(dto, dto.EntityTypeName))
            {
                _logger.LogWarning("Validation échouée pour la demande d'invitation : {Name} ({Type})", dto.Name, dto.EntityTypeName);
                throw new BusinessException(ErrorCodes.InvitationRequestInvalid, "validation");
            }
            
            // Validation de l'unicité des données (email, nom d'entité et BCE si Entreprise
            await _uniquenessValidator.ValidateUniqueEmailInInvitationRequestsAsync(dto.InvitationEmail,"RequestInvitationEmail");
            await _uniquenessValidator.ValidateUniqueEmailInUsersAsync(dto.Name,"RequestInvitationEmail");
            await _uniquenessValidator.ValidateUniqueEmailInSuperAdminInvitationAsync(dto.Name,"RequestInvitationEmail");
            
            // Unicité du nom de l'entité
            await _uniquenessValidator.ValidateUniqueEntityNameInEntitiesAsync(dto.Name);
            await _uniquenessValidator.ValidateUniqueEntityNameInInvitationRequestsAsync(dto.Name);

            // Si c'est une entreprise : Vérification de l'unicité du numero bce
            if (dto.EntityTypeName == "Company" && !string.IsNullOrWhiteSpace(dto.CompanyNumber))
            {
                await _uniquenessValidator.ValidateUniqueCompanyNumberAsync(dto.CompanyNumber);
            }
            
            // Si c'est une institution vérifier que l'institutionTypeExiste
            if (dto.EntityTypeName == "Institution")
            {
                if (!dto.InstitutionTypeId.HasValue ||
                    !await _institutionTypeRepository.ExistsByIdAsync(dto.InstitutionTypeId.Value))
                {
                    _logger.LogWarning("Le type d'institution {InstitutionTypeId} est invalide ou manquant pour {InstitutionName}.",
                        dto.InstitutionTypeId, dto.Name);
                    throw new BusinessException(ErrorCodes.InstitutionTypeNotFound, "institutionTypeId");
                }
            }


            // Vérification de l'existance du type de l'entité
            var entityType = await _entityTypeRepository.GetByNameAsync(dto.EntityTypeName)
                             ?? throw new BusinessException(ErrorCodes.InvitationRequestEntityTypeMissing, "entityTypeId");
            
            // Enregistrer la demande d'inscription
            var entity = _mapper.Map<InvitationRequest>(dto);

            // la langue pour les emails
            if (!dto.LanguageId.HasValue)
            {
                var spokenLang = await _spokenLanguageRepository.GetByCodeAsync(languageCode);

                if (spokenLang != null)
                {
                    entity.LanguageId = spokenLang.LanguageId;
                }
                else
                {
                    var fallbackLang = await _spokenLanguageRepository.GetByCodeAsync("fr");
                    entity.LanguageId = fallbackLang?.LanguageId ?? throw new BusinessException("LANGUAGE_FALLBACK_NOT_FOUND");
                }
            }
            else
            {
                entity.LanguageId = dto.LanguageId.Value;
            }
            
            entity.EntityTypeId = entityType.EntityTypeId;
            entity.SubmittedIp = ipAddress;
            entity.UserAgent = userAgent;
            entity.UpdatedAt = DateTime.Now;

            await _invitationRequestRepository.AddAsync(entity);

            _logger.LogInformation("Demande d'invitation enregistrée avec succès pour {Name}", dto.Name);
        }
        catch (BusinessException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la soumission d'une demande d'invitation pour {Name}", dto.Name);
            throw new BusinessException(ErrorCodes.InvitationRequestFailed);
        }
    }
}