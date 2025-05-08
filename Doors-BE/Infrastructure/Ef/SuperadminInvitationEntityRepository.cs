using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Ef.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Ef;

public class SuperadminInvitationEntityRepository : ISuperadminInvitationEntityRepository
{
    private readonly DoorsDbContext _context;

    public SuperadminInvitationEntityRepository(DoorsDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task AddAsync(SuperadminInvitationEntity entity)
    {
        await _context.SuperadminInvitationEntities.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<SuperadminInvitationEntity> GetByInvitationIdAsync(int invitationId)
    {
        return await _context.SuperadminInvitationEntities
            .Include(e => e.Entity)
            .FirstAsync(e => e.SuperadminInvitationId == invitationId);
    }


    public async Task DeleteAsync(int id)
    {
        var entity = await _context.SuperadminInvitationEntities
            .FirstOrDefaultAsync(e => e.SuperadminInvitationEntityId == id);
        if (entity != null)
        {
            _context.SuperadminInvitationEntities.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}