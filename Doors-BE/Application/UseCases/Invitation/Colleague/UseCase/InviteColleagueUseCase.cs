using Application.UseCases.Invitation.Colleague.DTOs;
using Application.UseCases.Invitation.Service;
using Domain.Exceptions;
using Domain.Interfaces;
using Infrastructure.Ef.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Invitation.Colleague.UseCase;

public class InviteColleagueUseCase
{
    private readonly IInvitationEmailService _invitationEmailService;
    private readonly IInvitationService _invitationService;
    private readonly IInvitationTypeRepository _invitationTypeRepository;
    private readonly ILogger<InviteColleagueUseCase> _logger;
    private readonly IRoleRepository _roleRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public InviteColleagueUseCase(
        IInvitationService invitationService,
        IInvitationEmailService invitationEmailService,
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IUnitOfWork unitOfWork,
        ILogger<InviteColleagueUseCase> logger,
        IInvitationTypeRepository invitationTypeRepository)
    {
        _invitationService = invitationService ?? throw new ArgumentNullException(nameof(invitationService));
        _invitationEmailService =
            invitationEmailService ?? throw new ArgumentNullException(nameof(invitationEmailService));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _invitationTypeRepository = invitationTypeRepository ??
                                    throw new ArgumentNullException(nameof(invitationTypeRepository));
    }

    public async Task<string> ExecuteAsync(InviteColleagueRequestDto request, int invitingUserId,string languageCode)
    {
        _logger.LogInformation("Inviting colleague {Email} by {InvitingUserId}", request.Email, invitingUserId);

        var invitingUser = await _userRepository.GetByIdAsync(invitingUserId);
        if (invitingUser == null || invitingUser.EntityUser == null)
        {
            _logger.LogWarning("Inviting user {InvitingUserId} not found or not linked to an entity", invitingUserId);
            throw new BusinessException(ErrorCodes.InviterNotLinkedToEntity, "invitingUserId");
        }

        var entityId = invitingUser.EntityUser.EntityId;
        var invitationTypeId = await _invitationTypeRepository.GetIdByNameAsync("Colleague");

        using var transaction = await _unitOfWork.BeginTransactionAsync();
        try
        {
            var (_, rawToken) = await _invitationService.CreateColleagueInvitationAsync(
                request.Email, entityId, request.RoleId, invitingUserId, invitationTypeId);

            var entityName = invitingUser.EntityUser.Entity.Name;
            var entityTypeName = invitingUser.EntityUser.Entity.EntityType.Name;
            var roleName = (await _roleRepository.GetByIdAsync(request.RoleId))?.Name ?? "Utilisateur";

            await _invitationEmailService.SendInvitationEmailAsync(
                request.Email,
                rawToken,
                entityName,
                entityTypeName,
                roleName,
                "ColleagueInvitationEmail",
                true,languageCode); 

            await _unitOfWork.SaveChangesAsync();
            await transaction.CommitAsync();

            _logger.LogInformation("Successfully invited colleague {Email} by {InvitingUserId}", request.Email,
                invitingUserId);
            return rawToken;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "Failed to invite colleague {Email} by {InvitingUserId}", request.Email,
                invitingUserId);
            throw;
        }
    }
}