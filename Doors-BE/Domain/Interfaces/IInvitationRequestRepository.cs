using Domain.Entities;
using Domain.Enums;

namespace Domain.Interfaces;

public interface IInvitationRequestRepository
{
    Task<InvitationRequest> AddAsync(InvitationRequest request);
    Task<InvitationRequest?> GetByIdAsync(int id);
    Task<bool> ExistEmailAsync(string invitationEmail);
    Task<IEnumerable<InvitationRequest>> GetPendingAsync();
    Task UpdateStatusAsync(int id, InvitationRequestStatus status, string? reason = null);

    Task<(int Total, List<InvitationRequest> Requests)> GetByStatusAsync(
        InvitationRequestStatus? status,
        string? entityTypeName,
        int page,
        int pageSize);

    Task<bool> ExistsNameInRequestsAsync(string name);
    Task<bool> ExistsCompanyNumberInRequestsAsync(string companyNumber);
}