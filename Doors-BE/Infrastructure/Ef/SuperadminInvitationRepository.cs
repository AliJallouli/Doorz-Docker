using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Ef.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Ef;

public class SuperadminInvitationRepository : ISuperadminInvitationRepository
{
    private readonly DoorsDbContext _context;

    public SuperadminInvitationRepository(DoorsDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task AddAsync(SuperadminInvitation invitation)
    {
        await _context.SuperadminInvitations.AddAsync(invitation);
        await _context.SaveChangesAsync();
    }

    public async Task<SuperadminInvitation> GetByIdAsync(int id)
    {
        return await _context.SuperadminInvitations
            .FirstAsync(i => i.SuperadminInvitationId == id);
    }

    public async Task<SuperadminInvitation?> GetByTokenAsync(string token)
    {
        return await _context.SuperadminInvitations
            .Include(i => i.InvitationType) // ✅ join de la table invitation_type
            .FirstOrDefaultAsync(i => i.InvitationToken == token);
    }


    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _context.SuperadminInvitations
            .AnyAsync(i => i.Email == email);
    }

    public async Task UpdateAsync(SuperadminInvitation invitation)
    {
        _context.SuperadminInvitations.Update(invitation);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var invitation = await GetByIdAsync(id);
        if (invitation != null)
        {
            _context.SuperadminInvitations.Remove(invitation);
            await _context.SaveChangesAsync();
        }
    }
}