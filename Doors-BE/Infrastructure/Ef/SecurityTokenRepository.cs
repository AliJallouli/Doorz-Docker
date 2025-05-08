using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Ef.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Ef;

public class SecurityTokenRepository : ISecurityTokenRepository
{
    private readonly DoorsDbContext _context;

    public SecurityTokenRepository(DoorsDbContext context)
    {
        _context = context;
    }

    public async Task<SecurityToken?> GetByTokenAsync(string tokenHash)
    {
        return await _context.SecurityTokens
            .Include(t => t.User)
            .Include(t => t.TokenType)
            .FirstOrDefaultAsync(t => t.TokenHash == tokenHash);
    }

    public async Task<IEnumerable<SecurityToken>> GetByUserIdAsync(int userId)
    {
        return await _context.SecurityTokens
            .Where(t => t.UserId == userId)
            .ToListAsync();
    }

    public async Task<SecurityToken?> GetLatestActiveByUserIdAndTypeAsync(int userId, string tokenTypeName)
    {
        return await _context.SecurityTokens
            .Include(t => t.TokenType)
            .Where(t =>
                t.UserId == userId &&
                !t.Used &&
                !t.Revoked &&
                t.TokenExpiresAt > DateTime.UtcNow &&
                t.TokenType.Name == tokenTypeName)
            .OrderByDescending(t => t.CreatedAt)
            .FirstOrDefaultAsync();
    }


    public async Task AddAsync(SecurityToken token)
    {
        _context.SecurityTokens.Update(token);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(SecurityToken token)
    {
        _context.SecurityTokens.Update(token);
        await _context.SaveChangesAsync();
    }

    public async Task RevokeAllForUserAsync(int userId)
    {
        var tokens = await _context.SecurityTokens
            .Where(t => t.UserId == userId && !t.Used && !t.Revoked)
            .ToListAsync();

        foreach (var token in tokens)
        {
            token.Revoked = true;
            token.RevokedAt = DateTime.UtcNow;
        }

        _context.SecurityTokens.UpdateRange(tokens);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsTokenValidAsync(string tokenHash)
    {
        return await _context.SecurityTokens
            .AnyAsync(t => t.TokenHash == tokenHash && !t.Used && !t.Revoked && t.TokenExpiresAt > DateTime.UtcNow);
    }

    public async Task UpdateRangeAsync(IEnumerable<SecurityToken> tokens)
    {
        _context.SecurityTokens.UpdateRange(tokens);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<SecurityToken>> GetValidTokensByTypeAsync(string tokenTypeName)
    {
        return await _context.SecurityTokens
            .Where(t =>
                !t.Used &&
                !t.Revoked &&
                t.TokenExpiresAt > DateTime.UtcNow &&
                t.TokenType.Name == tokenTypeName)
            .Include(t => t.TokenType)
            .ToListAsync();
    }

    public async Task<IEnumerable<SecurityToken>> GetActiveByUserIdAndTypeAsync(int userId, string tokenTypeName)
    {
        return await _context.SecurityTokens
            .Include(t => t.TokenType)
            .Where(t =>
                t.UserId == userId &&
                !t.Used &&
                !t.Revoked &&
                t.TokenExpiresAt > DateTime.UtcNow &&
                t.TokenType.Name == tokenTypeName)
            .ToListAsync();
    }

    public async Task<IEnumerable<SecurityToken>> GetByUserIdAndTypeWithinWindow(int userId, string tokenTypeName,
        DateTime minCreatedAt)
    {
        return await _context.SecurityTokens
            .Include(t => t.TokenType)
            .Where(t =>
                t.UserId == userId &&
                !t.Used &&
                !t.Revoked &&
                t.TokenExpiresAt > DateTime.UtcNow &&
                t.CreatedAt >= minCreatedAt &&
                t.TokenType.Name == tokenTypeName)
            .ToListAsync();
    }


    public async Task<IEnumerable<SecurityToken>> GetValidTokensByTypeAndCreatedAfterAsync(string tokenTypeName,
        DateTime minCreatedAt)
    {
        var tokens = await _context.SecurityTokens
            .AsNoTracking()
            .Include(t => t.TokenType)
            .Where(t =>
                !t.Used &&
                !t.Revoked &&
                t.TokenExpiresAt > DateTime.UtcNow &&
                t.CreatedAt >= minCreatedAt &&
                t.TokenType.Name == tokenTypeName)
            .ToListAsync();
        return tokens;
    }
    public async Task<IEnumerable<SecurityToken>> GetAllTokensByTypeAsync(string tokenTypeName)
    {
        return await _context.SecurityTokens
            .Include(t => t.TokenType)
            .Where(t => t.TokenType.Name == tokenTypeName)
            .ToListAsync();
    }

}