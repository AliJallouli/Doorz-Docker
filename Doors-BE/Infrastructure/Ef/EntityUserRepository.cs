using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Ef.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Ef;

public class EntityUserRepository : IEntityUserRepository
{
    private readonly DoorsDbContext _context;

    public EntityUserRepository(DoorsDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task AddAsync(EntityUser entityUser)
    {
        _context.EntityUsers.Add(entityUser);
        await _context.SaveChangesAsync();
    }

    public async Task<EntityUser?> GetByIdAsync(int entityUserId)
    {
        return await _context.EntityUsers
            .FirstOrDefaultAsync(eu => eu.EntityUserId == entityUserId);
    }

    public async Task<EntityUser?> GetByUserAndEntityAsync(int userId, int entityId)
    {
        return await _context.EntityUsers
            .FirstOrDefaultAsync(eu => eu.UserId == userId &&
                                       eu.EntityId == entityId);
    }

    public async Task<EntityUser?> GetByUserIdAsync(int userId)
    {
        return await _context.EntityUsers
            .FirstOrDefaultAsync(eu => eu.UserId == userId);
    }

    public async Task UpdateAsync(EntityUser entityUser)
    {
        _context.EntityUsers.Update(entityUser);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int entityUserId)
    {
        var entityUser = await _context.EntityUsers.FindAsync(entityUserId);
        if (entityUser != null)
        {
            _context.EntityUsers.Remove(entityUser);
            await _context.SaveChangesAsync();
        }
    }
}