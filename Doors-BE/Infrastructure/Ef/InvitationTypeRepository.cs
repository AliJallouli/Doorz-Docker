using Domain.Interfaces;
using Infrastructure.Ef.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Ef;

public class InvitationTypeRepository : IInvitationTypeRepository
{
    private readonly DoorsDbContext _context;

    public InvitationTypeRepository(DoorsDbContext context)
    {
        _context = context;
    }

    public async Task<int> GetIdByNameAsync(string name)
    {
        var type = await _context.InvitationTypes
            .Where(t => t.Name == name)
            .FirstOrDefaultAsync();

        if (type == null)
            throw new InvalidOperationException($"Invitation type '{name}' not found.");

        return type.InvitationTypeId;
    }
}