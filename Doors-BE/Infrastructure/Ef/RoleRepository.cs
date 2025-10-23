using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Ef.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Ef;

public class RoleRepository : IRoleRepository
{
    private readonly DoorsDbContext _context;

    public RoleRepository(DoorsDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task AddAsync(Role role)
    {
        await _context.Roles.AddAsync(role);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int roleId, int entityTypeId)
    {
        return await _context.Roles
            .AnyAsync(r => r.RoleId == roleId && r.EntityTypeId == entityTypeId);
    }

    public async Task<Role?> GetByIdAsync(int roleId)
    {
        return await _context.Roles
            .Include(r => r.EntityType) 
            .FirstOrDefaultAsync(r => r.RoleId == roleId);
    }

    public async Task<Role?> GetByNameAndEntityTypeAsync(string name, int entityTypeId)
    {
        return await _context.Roles
            .Include(r => r.EntityType) 
            .FirstOrDefaultAsync(r => r.Name == name && r.EntityTypeId == entityTypeId);
    }

    public async Task<List<Role>> GetRolesByEntityTypeIdAsync(int entityTypeId)
    {
        return await _context.Roles
            .Where(r => r.EntityTypeId == entityTypeId)
            .ToListAsync();
    }

    public async Task<List<Role>> GetRolesByEntityTypeNameAsync(string entityTypeName, string language)
    {
        var roles = await _context.Roles
            .Include(r => r.EntityType) 
            .Include(r => r.Translations) 
            .ThenInclude(t => t.Language) 
            .Where(r => r.EntityType.Name == entityTypeName)
            .ToListAsync();

        foreach (var role in roles)
        {
            var translation = role.Translations
                                  .FirstOrDefault(t =>
                                      t.Language.Code == language) 
                              ?? role.Translations.FirstOrDefault(t => t.Language.Code == "en"); 

            if (translation != null)
            {
                role.Name = translation.TranslatedName; 
                role.Description = translation.TranslatedDescription; 
            }
           
        }

        return roles;
    }

    public async Task<int?> GetRoleIdByRoleNameAndEntityTypeName(string roleName, string entityTypeName)
    {
        return await _context.Roles
            .Where(r => r.Name == roleName && r.EntityType.Name == entityTypeName)
            .Select(r => (int?)r.RoleId)
            .FirstOrDefaultAsync();
    }
}