using Domain.Interfaces.Services;
using Infrastructure.Ef.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Ef;

public class SuperRoleRepository : ISuperRoleRepository
{
    private readonly DoorsDbContext _context;

    public SuperRoleRepository(DoorsDbContext context)
    {
        _context = context;
    }

    public Task<int> GetSuperRoleIdAsync(string roleName)
    {
        return _context.SuperRoles
            .Where(r => r.Name == roleName)
            .Select(r => r.SuperRoleId)
            .FirstOrDefaultAsync();
    }
}