using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Ef.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Ef;

public class UserRepository : IUserRepository
{
    private readonly DoorsDbContext _context;

    public UserRepository(DoorsDbContext context)
    {
        _context = context;
    }

    public async Task<Users?> GetByIdAsync(int userId)
    {
        // Récupère d'abord l'utilisateur avec son SuperRole uniquement
        var user = await _context.Users
            .Include(u => u.SuperRole)
            .FirstOrDefaultAsync(u => u.UserId == userId);

        if (user == null) return null; // Utilisateur non trouvé

        // Vérifie si l'utilisateur est un superadmin
        var isSuperAdmin = user.SuperRole?.Name == "SuperAdmin"; // Remplace "SuperAdmin" par le nom exact de ton rôle

        if (!isSuperAdmin)
            user = await _context.Users
                .Include(u => u.SuperRole)
                .Include(u => u.EntityUser)
                .ThenInclude(eu => eu.Role)
                .Include(u => u.EntityUser)
                .ThenInclude(eu => eu.Entity)
                .ThenInclude(e => e.EntityType)
                .FirstOrDefaultAsync(u => u.UserId == userId);

        return user;
    }

    /// <summary>
    ///     Récupère un utilisateur par son email, incluant son rôle global (SuperRole) et, si nécessaire, ses rôles
    ///     spécifiques via EntityUser.
    ///     Si l'utilisateur est un superadmin, seules les informations de base et le SuperRole sont incluses.
    /// </summary>
    /// <param name="email">L'email de l'utilisateur à rechercher.</param>
    /// <returns>L'utilisateur trouvé ou null s'il n'existe pas.</returns>
    /// <exception cref="ArgumentNullException">Lancée si l'email est null ou vide.</exception>
    public async Task<Users?> GetByEmailAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentNullException(nameof(email), "L'email ne peut pas être null ou vide.");

        // Récupère d'abord l'utilisateur avec son SuperRole uniquement
        var user = await _context.Users
            .Include(u => u.SuperRole)
            .FirstOrDefaultAsync(u => u.Email == email);

        if (user == null) return null; // Utilisateur non trouvé

        // Vérifie si l'utilisateur est un superadmin
        var isSuperAdmin = user.SuperRole?.Name == "SuperAdmin"; // Remplace "SuperAdmin" par le nom exact de ton rôle

        if (!isSuperAdmin)
            user = await _context.Users
                .Include(u => u.SuperRole)
                .Include(u => u.EntityUser)
                .ThenInclude(eu => eu.Role)
                .Include(u => u.EntityUser)
                .ThenInclude(eu => eu.Entity)
                .ThenInclude(e => e.EntityType)
                .FirstOrDefaultAsync(u => u.Email == email);

        return user;
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _context.Users
            .AnyAsync(i => i.Email == email);
    }

    public async Task<Users> AddAsync(Users users)
    {
        _context.Users.Add(users);
        await _context.SaveChangesAsync();
        return users;
    }

    public async Task UpdateAsync(Users users)
    {
        Console.WriteLine($"Début UpdateAsync pour userId: {users.UserId}, IsVerified: {users.IsVerified}");
        _context.Entry(users).State = EntityState.Modified; // Marque explicitement comme modifiée
        var changes = await _context.SaveChangesAsync();
        Console.WriteLine($"Fin UpdateAsync, lignes mises à jour: {changes}");
    }

    public async Task DeleteAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
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

    public async Task AddRefreshTokenAsync(RefreshToken refreshToken)
    {
        _context.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateLastLoginAsync(int userId, DateTime lastLogin)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user != null)
        {
            user.LastLoginAt = lastLogin;
            await _context.SaveChangesAsync();
        }
    }

    public async Task AddLoginAttemptAsync(LoginAttempt attempt)
    {
        _context.LoginAttempts.Add(attempt);
        await _context.SaveChangesAsync();
    }

    public async Task<int> GetFailedLoginAttemptsCountAsync(string email, TimeSpan timeSpan)
    {
        var threshold = DateTime.UtcNow - timeSpan;
        return await _context.LoginAttempts
            .CountAsync(la => la.Email == email && la.AttemptTime >= threshold && !la.Success);
    }

    public async Task<bool> RemoveRefreshTokenAsync(int userId, string refreshToken)
    {
        var token = await _context.RefreshTokens
            .FirstOrDefaultAsync(t => t.UserId == userId && t.Token == refreshToken);
        if (token == null) return false;

        _context.RefreshTokens.Remove(token);
        return true;
    }

    public async Task RemoveAllRefreshTokensAsync(int userId)
    {
        var tokens = _context.RefreshTokens.Where(t => t.UserId == userId);
        _context.RefreshTokens.RemoveRange(tokens);
        await Task.CompletedTask; // Pas de sauvegarde ici, géré par UnitOfWork
    }

    public async Task AddSessionEventAsync(SessionEvent sessionEvent)
    {
        if (sessionEvent == null)
            throw new ArgumentNullException(nameof(sessionEvent));

        await _context.SessionEvents.AddAsync(sessionEvent);
        await _context.SaveChangesAsync();
    }
    public async Task RemoveRefreshTokensBySessionAsync(int sessionEventId)
    {
        await _context.RefreshTokens
            .Where(rt => rt.SessionEventId == sessionEventId)
            .ExecuteDeleteAsync();
    }
}