using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Ef.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Ef;

public class SessionEventRepository: ISessionEventRepository
{
    private readonly DoorsDbContext _context;

    public SessionEventRepository(DoorsDbContext dbContext)
    {
        _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<SessionEvent?> GetByIdAsync(int sessionEventId)
    {
        return await _context.SessionEvents
            .FirstOrDefaultAsync(se => se.SessionEventId == sessionEventId);
    }
    public async Task<bool> GetRememberMeByRefreshTokenAsync(string refreshToken)
    {
        var sessionEvent = await _context.RefreshTokens
            .AsNoTracking()
            .Where(rt => rt.Token == refreshToken)
            .Join(
                _context.SessionEvents,
                rt => rt.SessionEventId,
                se => se.SessionEventId,
                (rt, se) => se
            )
            .Select(se => new { se.RememberMe })
            .FirstOrDefaultAsync();

        return sessionEvent!= null?sessionEvent.RememberMe: false;
    }
    
     public async Task<List<SessionEvent>> GetActiveSessionsByUserAsync(int userId)
    {
        return await _context.SessionEvents
            .Where(s => s.UserId == userId && !s.IsRevoked)
            .OrderByDescending(s => s.LastSeenAt ?? s.OpenedAt)
            .ToListAsync();
    }

    public async Task<int> CreateAsync(SessionEvent session)
    {
        _context.SessionEvents.Add(session);
        await _context.SaveChangesAsync();
        return session.SessionEventId;
    }
    
    public async Task<List<SessionEvent>> GetActiveSessionsByUserExceptAsync(int userId, int sessionIdToKeep)
    {
        return await _context.SessionEvents
            .Where(s => s.UserId == userId && !s.IsRevoked && s.SessionEventId != sessionIdToKeep)
            .ToListAsync();
    }
    
    public async Task UpdateLastSeenAsync(int sessionEventId)
    {
        var session = await _context.SessionEvents.FindAsync(sessionEventId);
        if (session == null || session.IsRevoked) return;

        session.LastSeenAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
    }
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
   
}