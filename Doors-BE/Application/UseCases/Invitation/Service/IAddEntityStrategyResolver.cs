using Application.UseCases.Invitation.SuperAdmin.DTOs;

namespace Application.UseCases.Invitation.Service;

public interface IAddEntityStrategyResolver
{
    Task<object> ExecuteAsync(ProcessInvitationRequestDto  processInvitationRequestDto , int createdBy);
}