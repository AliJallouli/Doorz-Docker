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
   
        var user = await _context.Users
            .Include(u => u.SuperRole)
            .FirstOrDefaultAsync(u => u.UserId == userId);

        if (user == null) return null; 

     
        var isSuperAdmin = user.SuperRole.Name == "SuperAdmin"; 

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
        var isSuperAdmin = user.SuperRole?.Name == "SuperAdmin"; 

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
        if (users == null)
        {
            throw new ArgumentNullException(nameof(users), "L'entité utilisateur ne peut pas être nulle.");
        }

        try
        {
            // Vérifier si l'entité est déjà suivie
            var existingUser = _context.Users.Local.FirstOrDefault(u => u.UserId == users.UserId);
            if (existingUser != null)
            {
                // Mettre à jour les propriétés de l'entité existante
                _context.Entry(existingUser).CurrentValues.SetValues(users);
            }
            else
            {
                // Attacher et marquer l'entité comme modifiée
                _context.Users.Update(users);
            }

            // Persister les modifications de manière asynchrone
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new InvalidOperationException($"Erreur de concurrence lors de la mise à jour de l'utilisateur avec l'ID {users.UserId}.", ex);
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException($"Erreur de base de données lors de la mise à jour de l'utilisateur avec l'ID {users.UserId}.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Erreur inattendue lors de la mise à jour de l'utilisateur avec l'ID {users.UserId}.", ex);
        }
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

    public async Task<int> GetFailedLoginAttemptsCountAsync(string email,string ipAddress, TimeSpan timeSpan)
    {
        var threshold = DateTime.UtcNow - timeSpan;
        return await _context.LoginAttempts
            .CountAsync(la => la.Email == email && la.IpAddress == ipAddress && la.AttemptTime >= threshold && !la.Success);
    }
   
}