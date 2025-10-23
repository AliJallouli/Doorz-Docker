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

public class AddStudentMovementUseCase:AddEntityUseCaseBase<CreateStudentMovementDto, StudentMovement, StudentMovementDto>
{
    private readonly IStudentMovementRepository _studentMovementRepository;
    private readonly ILogger<AddStudentMovementUseCase> _logger;

    public AddStudentMovementUseCase(
        IStudentMovementRepository studentMovementRepository,
        IInvitationService invitationService,
        IInvitationEmailService emailService,
        IRoleRepository roleRepository,
        IEntityTypeRepository entityTypeRepository,
        IEntityRepository entityRepository,
        IMapper mapper,
        ILogger<AddStudentMovementUseCase> logger,
        IInvitationTypeRepository invitationTypeRepository,
        ISharedUniquenessValidationService sharedUniquenessValidationService)
        : base(invitationService, emailService, roleRepository, entityTypeRepository, entityRepository, mapper, logger, invitationTypeRepository, sharedUniquenessValidationService)
    {
        _studentMovementRepository = studentMovementRepository ?? throw new ArgumentNullException(nameof(studentMovementRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    

  

    protected override Task ValidateDataAsync(CreateStudentMovementDto dto)
    {
        _logger.LogDebug("Validation des données supplémentaires pour {StudentMovementName} (aucune validation spécifique).", dto.Name);
        return Task.CompletedTask;
    }

    protected override async Task<StudentMovement> CreateSpecificEntityAsync(CreateStudentMovementDto dto, Entity entity, int createdBy)
    {
        _logger.LogInformation("Création d'un mouvement étudiant spécifique : {StudentMovementName}", dto.Name);

        var studentMovement = new StudentMovement
        {
            EntityId = entity.EntityId,
            Name = dto.Name,
            CreatedBy = createdBy,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        try
        {
            await _studentMovementRepository.AddAsync(studentMovement);
            _logger.LogDebug("Mouvement étudiant {StudentMovementName} ajouté avec succès, ID : {StudentMovementId}", studentMovement.Name, studentMovement.StudentMovementId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Échec de l'ajout du mouvement étudiant {StudentMovementName} à la base de données.", dto.Name);
            throw new BusinessException(ErrorCodes.StudentMovementCreationFailed);
        }

        return studentMovement;
    }

    protected override string GetEntityTypeName()
    {
        return "StudentMovement";
    }

    protected override string GetInvitationEmail(CreateStudentMovementDto dto)
    {
        _logger.LogDebug("Récupération de l'email d'invitation : {Email}", dto.InvitationEmail);
        return dto.InvitationEmail;
    }

    protected override int GetEntityId(StudentMovement entity)
    {
        _logger.LogDebug("Récupération de l'ID du mouvement étudiant : {StudentMovementId}", entity.StudentMovementId);
        return entity.StudentMovementId;
    }

    protected override StudentMovementDto MapToDto(StudentMovement entity, Role role)
    {
        _logger.LogDebug("Mappage du mouvement étudiant {StudentMovementId} et du rôle {RoleId} en DTO.", entity.StudentMovementId, role.RoleId);

        var dto = Mapper.Map<StudentMovementDto>(entity);
        dto.Role = Mapper.Map<RoleDto>(role);

        _logger.LogDebug("Mappage terminé pour le mouvement étudiant {StudentMovementId}", entity.StudentMovementId);
        return dto;
    }

    public override async Task<StudentMovementDto> ExecuteAsync(CreateStudentMovementDto dto, int createdBy, string languageCode)
    {
        _logger.LogInformation("Début de l'ajout du mouvement étudiant {StudentMovementName} par l'utilisateur {CreatedBy}", dto.Name, createdBy);

        try
        {
            var result = await base.ExecuteAsync(dto, createdBy, languageCode);
            _logger.LogInformation("Mouvement étudiant {StudentMovementName} ajouté avec succès par l'utilisateur {CreatedBy}", dto.Name, createdBy);
            return result;
        }
        catch (BusinessException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Échec de l'ajout du mouvement étudiant {StudentMovementName} par l'utilisateur {CreatedBy}", dto.Name, createdBy);
            throw new BusinessException(ErrorCodes.StudentMovementCreationFailed);
        }
    }
}