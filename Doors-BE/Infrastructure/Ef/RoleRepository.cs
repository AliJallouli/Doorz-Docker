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
            .Include(r => r.EntityType) // Inclut la navigation vers EntityType si nécessaire
            .FirstOrDefaultAsync(r => r.RoleId == roleId);
    }

    public async Task<Role?> GetByNameAndEntityTypeAsync(string name, int entityTypeId)
    {
        return await _context.Roles
            .Include(r => r.EntityType) // Optionnel, selon si vous avez besoin de EntityType
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
            .Include(r => r.EntityType) // Charge la relation EntityType
            .Include(r => r.Translations) // Charge les traductions
            .ThenInclude(t => t.Language) // Charge la langue associée à chaque traduction
            .Where(r => r.EntityType.Name == entityTypeName)
            .ToListAsync();

        foreach (var role in roles)
        {
            var translation = role.Translations
                                  .FirstOrDefault(t =>
                                      t.Language.Code == language) // Recherche la traduction pour la langue demandée
                              ?? role.Translations.FirstOrDefault(t => t.Language.Code == "en"); // Fallback sur anglais

            if (translation != null)
            {
                role.Name = translation.TranslatedName; // Remplace le nom technique par le traduit
                role.Description = translation.TranslatedDescription; // Remplace la description par la traduite
            }
            // Si aucune traduction n’est trouvée, on garde les valeurs par défaut de role.Name et role.Description
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