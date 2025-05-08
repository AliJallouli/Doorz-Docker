using Application.UseCases.Auth.DTOs;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Auth.UseCases.Register;

public class ValidateInvitationTokenUseCase
{
    private readonly ISuperadminInvitationEntityRepository _invitationEntityRepository;
    private readonly ISuperadminInvitationRepository _invitationRepository;
    private readonly ILogger _logger;
    private readonly ITokenHasher _tokenHasher;
    private readonly IUserRepository _userRepository;

    public ValidateInvitationTokenUseCase(
        ISuperadminInvitationRepository superadminInvitationRepository,
        ILogger<ValidateInvitationTokenUseCase> logger,
        ISuperadminInvitationEntityRepository superadminInvitationEntityRepository,
        IUserRepository userRepository,
        ITokenHasher tokenHasher)
    {
        _invitationRepository = superadminInvitationRepository ??
                                throw new ArgumentNullException(nameof(superadminInvitationRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _invitationEntityRepository = superadminInvitationEntityRepository ??
                                      throw new ArgumentNullException(nameof(superadminInvitationEntityRepository));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _tokenHasher = tokenHasher;
    }

    public async Task<ValidateInvitationTokenResponseDto> ExecuteAsync(ValidateInvitationTokenRequestDto request)
    {
        if (string.IsNullOrEmpty(request.InvitationToken))
        {
            _logger.LogInformation("Invitation invalide ou expirée pour le token {Token}", request.InvitationToken);
            throw new BusinessException(ErrorCodes.InvitationInvalid, "invitationToken");
        }

        // Récupère l'invitation à partir du token fourni
        var hashedToken = _tokenHasher.HashToken(request.InvitationToken);
        if (string.IsNullOrEmpty(hashedToken))
        {
            _logger.LogWarning("Invitation invalide ou expirée pour le token {Token}", hashedToken);
            throw new BusinessException(ErrorCodes.InvitationInvalid, "invitationToken");
        }

        var invitation = await _invitationRepository.GetByTokenAsync(hashedToken);

        if (invitation == null || invitation.Used || invitation.ExpiresAt < DateTime.UtcNow)
        {
            _logger.LogWarning("Invitation invalide ou expirée pour le token {Token}", request.InvitationToken);
            throw new BusinessException(ErrorCodes.InvitationInvalid, "invitationToken");
        }

        // Vérifie les détails de l'invitation (entité et rôle)
        var invitationEntity =
            await _invitationEntityRepository.GetByInvitationIdAsync(invitation.SuperadminInvitationId);

        if (invitationEntity == null)
        {
            _logger.LogWarning("Aucune entité associée à l'invitation {InvitationId}",
                invitation.SuperadminInvitationId);
            throw new BusinessException(ErrorCodes.InvitationDetailsMissing, "invitationEntity");
        }

        if (invitationEntity.Entity == null)
        {
            _logger.LogCritical("💥 invitationEntity.Entity est null pour l'invitation {Id}",
                invitation.SuperadminInvitationId);
            throw new BusinessException(ErrorCodes.InvitationDetailsMissing, "entity");
        }


        if (invitationEntity.RoleId <= 0 || invitationEntity.Entity.EntityTypeId <= 0 ||
            invitationEntity.Entity.EntityId <= 0)
        {
            _logger.LogCritical("🚨 Invitation insérée sans RoleId valide. ID: {InvitationId}",
                invitation.SuperadminInvitationId);
            throw new BusinessException(ErrorCodes.InvitationDetailsMissing, "RoleId");
        }

        var existingUser = await _userRepository.GetByEmailAsync(invitation.Email);
        if (existingUser != null)
        {
            _logger.LogWarning("L'email {Email} est déjà utilisé par un utilisateur existant.", invitation.Email);
            throw new BusinessException(ErrorCodes.EmailAlreadyUsed, "email");
        }


        return new ValidateInvitationTokenResponseDto
        {
            InvitationToken = invitation.InvitationToken,
            Email = invitation.Email,
            EntityTypeId = invitationEntity.Entity.EntityTypeId,
            RoleId = invitationEntity.RoleId,
            EntityId = invitationEntity.EntityId,
            InvitationType = invitation.InvitationType.Name
        };
    }
}