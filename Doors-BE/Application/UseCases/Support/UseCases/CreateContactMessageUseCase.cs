using Application.UseCases.Auth.Service;
using Application.UseCases.Support.DTOs;
using Application.UseCases.Support.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Support;
using Domain.Interfaces;
using Domain.Interfaces.Support;
using Infrastructure.Ef.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Support.UseCases;

public class CreateContactMessageUseCase
{
    private readonly IContactMessageRepository _contactMessageRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserRepository _userRepo;
    private readonly IContactEmailService _contactEmailService;
    private readonly ISpokenLanguageRepository _spokenLanguageRepo;
    private readonly IContactMessageTypeRepository _contactMessageTypeRepo;
    private readonly ILogger<CreateContactMessageUseCase> _logger;

    public CreateContactMessageUseCase(
        IContactMessageRepository contactMessageRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IAuthenticationService authenticationService,
        IUserRepository userRepo,
        IContactEmailService contactEmailService,
        ISpokenLanguageRepository spokenLanguageRepo,
        IContactMessageTypeRepository contactMessageTypeRepo,
        ILogger<CreateContactMessageUseCase> logger)
    {
        _contactMessageRepository = contactMessageRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _authenticationService = authenticationService;
        _userRepo = userRepo;
        _contactEmailService = contactEmailService;
        _spokenLanguageRepo = spokenLanguageRepo;
        _contactMessageTypeRepo = contactMessageTypeRepo;
        _logger = logger;
    }

public async Task ExecuteAsync(CreateContactMessageRequestDto dto, string userAgent, string ipAddress)
{
    _logger.LogInformation("Début de CreateContactMessageUseCase.ExecuteAsync pour {Email}, LanguageId: {LanguageId}", dto.Email, dto.LanguageId);

    try
    {
        var entity = _mapper.Map<ContactMessage>(dto);
        entity.ReceivedAt = DateTime.UtcNow;
        entity.IpAddress = ipAddress;

        _logger.LogInformation("Traitement de l'agent utilisateur");
        entity.UserAgentId = await _authenticationService.ProcessUserAgentAsync(userAgent);

        if (dto.UserId != null)
        {
            _logger.LogInformation("Récupération de l'utilisateur avec UserId: {UserId}", dto.UserId);
            var user = await _userRepo.GetByIdAsync(dto.UserId.Value);
            if (user != null)
            {
                entity.FullName = $"{user.FirstName} {user.LastName}";
                entity.Email = user.Email;
                entity.UserId = user.UserId;
            }
        }

        if (string.IsNullOrWhiteSpace(entity.Subject))
        {
            _logger.LogInformation("Définition du sujet pour ContactMessageTypeId: {ContactMessageTypeId}", entity.ContactMessageTypeId);
            if (entity.ContactMessageTypeId.HasValue)
            {
                var type = await _contactMessageTypeRepo.GetByIdAsync(entity.ContactMessageTypeId.Value);
                var translation = type?.Translations.FirstOrDefault(t => t.LanguageId == entity.LanguageId);
                entity.Subject = translation?.Name ?? "Sans sujet";
            }
            else
            {
                entity.Subject = "Sans sujet";
            }
        }

        _logger.LogInformation("Enregistrement du message dans la base de données");
        await _contactMessageRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Récupération du code de langue pour LanguageId: {LanguageId}", dto.LanguageId);
        var languageCode = await _spokenLanguageRepo.GetByIdAsync(dto.LanguageId);
        if (languageCode == null)
        {
            _logger.LogError("Langue non trouvée pour LanguageId: {LanguageId}", dto.LanguageId);
            throw new ApplicationException($"Langue non trouvée pour LanguageId: {dto.LanguageId}");
        }

        _logger.LogInformation("Appel de NotifySupportAsync");
        await _contactEmailService.NotifySupportAsync(entity, "fr", userAgent);

        _logger.LogInformation("Appel de AcknowledgeUserAsync avec LanguageCode: {LanguageCode}", languageCode.Code);
        await _contactEmailService.AcknowledgeUserAsync(entity, languageCode.Code);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Erreur dans CreateContactMessageUseCase.ExecuteAsync pour {Email}", dto.Email);
        throw;
    }
}
}