using Domain.Entities;

namespace Application.UseCases.Invitation.Service;

public interface IInvitationService
{
    Task<(SuperadminInvitation invitation, EntityType entityType, string rawToken)> CreateInvitationAsync(
        string email, int entityId, string entityTypeName, int roleId, int? createdBy, int invitationTypeId);

    Task<(SuperadminInvitation invitation, string rawToken)> CreateColleagueInvitationAsync(
        string email, int entityId, int roleId, int invitingUserId, int invitationTypeId);
}