using Domain.Entities.Legals;
using Domain.Interfaces.Legals;
using Infrastructure.Ef.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Ef.Legals;

public class UserLegalConsentRepository : IUserLegalConsentRepository
{
    private readonly DoorsDbContext _context;

    public UserLegalConsentRepository(DoorsDbContext context)
    {
        _context = context;
    }

    public async Task<UserLegalConsent?> GetActiveConsentAsync(int userId, int documentId)
    {
        return await _context.UserLegalConsents
            .FirstOrDefaultAsync(c => c.UserId == userId && c.DocumentId == documentId && !c.Revoked);
    }

    public async Task<List<UserLegalConsent>> GetByUserAsync(int userId)
    {
        return await _context.UserLegalConsents
            .Where(c => c.UserId == userId)
            .ToListAsync();
    }

    public async Task AddAsync(UserLegalConsent consent)
    {
        await _context.UserLegalConsents.AddAsync(consent);
    }

    public async Task RevokeConsentAsync(UserLegalConsent consent, string reason)
    {
        consent.Revoked = true;
        consent.RevokedAt = DateTime.UtcNow;
        consent.RevokeReason = reason;
        _context.UserLegalConsents.Update(consent);
    }
}
