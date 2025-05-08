using Domain.Entities.Legals;

namespace Domain.Interfaces.Legals;

public interface IUserLegalConsentRepository
{
    Task<UserLegalConsent?> GetActiveConsentAsync(int userId, int documentId);
    Task<List<UserLegalConsent>> GetByUserAsync(int userId);
    Task AddAsync(UserLegalConsent consent);
    Task RevokeConsentAsync(UserLegalConsent consent, string reason);
}