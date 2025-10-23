using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Ef.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Ef;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly DoorsDbContext _context;

    public RefreshTokenRepository(DoorsDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public async Task<RefreshToken?> GetRefreshTokenAsync(string token)
    {
        return await _context.RefreshTokens
            .FromSqlRaw("SELECT * FROM refresh_token WHERE token = {0} FOR UPDATE", token)
            .AsNoTracking()
            .OrderBy(t => t.RefreshTokenId) 
            .Take(1)
            .SingleOrDefaultAsync();
    }

    public async Task AddRefreshTokenAsync(RefreshToken refreshToken)
    {
        _context.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync();
    }

    public async Task<RefreshToken?> GetValidTokenBySessionAsync(int sessionEventId)
    {
        return await _context.RefreshTokens
            .Where(t => t.SessionEventId == sessionEventId && !t.Used && t.ExpiresAt > DateTime.UtcNow)
            .AsNoTracking()
            .SingleOrDefaultAsync();
    }

    public async Task AddAsync(RefreshToken refreshToken)
    {
        _context.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAllForUserAsync(int userId)
    {
        var tokens = _context.RefreshTokens.Where(rt => rt.UserId == userId);
        _context.RefreshTokens.RemoveRange(tokens);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(RefreshToken token)
    {
        _context.RefreshTokens.Update(token);
        await _context.SaveChangesAsync();
    }
    public async Task<RefreshToken?> GetLatestValidBySessionAsync(int sessionEventId)
    {
        return await _context.RefreshTokens
            .Where(r => r.SessionEventId == sessionEventId && !r.Used && r.ExpiresAt > DateTime.UtcNow)
            .OrderByDescending(r => r.CreatedAt)
            .FirstOrDefaultAsync();
    }


    public async Task RemoveRefreshTokensBySessionAsync(int sessionEventId)
    {
        await _context.RefreshTokens
            .Where(rt => rt.SessionEventId == sessionEventId)
            .ExecuteDeleteAsync();
    }
    
    public async Task DeleteRefreshTokensBySessionIdAsync(int sessionId)
    {
        var tokens = await _context.RefreshTokens
            .Where(t => t.SessionEventId == sessionId)
            .ToListAsync();

        if (tokens.Any())
        {
            _context.RefreshTokens.RemoveRange(tokens);
        }
    }

    public async Task DeleteRefreshTokensByUserIdAsync(int userId)
    {
        var tokens = await _context.RefreshTokens
            .Join(_context.SessionEvents,
                token => token.SessionEventId,
                session => session.SessionEventId,
                (token, session) => new { token, session })
            .Where(joined => joined.session.UserId == userId)
            .Select(joined => joined.token)
            .ToListAsync();

        if (tokens.Any())
        {
            _context.RefreshTokens.RemoveRange(tokens);
        }
    }

    public async Task DeleteRefreshTokensByUserIdExceptAsync(int userId, int sessionIdToKeep)
    {
        var tokens = await _context.RefreshTokens
            .Join(_context.SessionEvents,
                token => token.SessionEventId,
                session => session.SessionEventId,
                (token, session) => new { token, session })
            .Where(joined => joined.session.UserId == userId && joined.session.SessionEventId != sessionIdToKeep)
            .Select(joined => joined.token)
            .ToListAsync();

        if (tokens.Any())
        {
            _context.RefreshTokens.RemoveRange(tokens);
        }
    }
}