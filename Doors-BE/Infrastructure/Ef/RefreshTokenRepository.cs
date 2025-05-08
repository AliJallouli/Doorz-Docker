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

    public async Task DeleteAsync(int refreshTokenId)
    {
        var token = await _context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.RefreshTokenId == refreshTokenId);
        if (token != null)
        {
            _context.RefreshTokens.Remove(token);
            await _context.SaveChangesAsync(); // Assurez-vous de sauvegarder les changements
        }
    }
    public async Task<RefreshToken?> GetRefreshTokenAsync(string token)
    {
        return await _context.RefreshTokens
            .FromSqlRaw("SELECT * FROM refresh_token WHERE token = {0} FOR UPDATE", token)
            .AsNoTracking()
            .OrderBy(t => t.RefreshTokenId) // Ajout pour éliminer l'avertissement EF Core
            .Take(1)
            .SingleOrDefaultAsync();
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


}