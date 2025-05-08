using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Ef.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Ef;

public class EntityTypeRepository : IEntityTypeRepository
{
    private readonly DoorsDbContext _context;

    public EntityTypeRepository(DoorsDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<EntityType> GetByNameAsync(string name)
    {
        return await _context.EntityTypes
            .FirstOrDefaultAsync(et => et.Name == name);
    }

    public async Task<EntityType> GetByIdAsync(int id)
    {
        return await _context.EntityTypes
            .FirstOrDefaultAsync(et => et.EntityTypeId == id);
    }

    public async Task<EntityType> GetByEntityIdAsync(int entityId)
    {
        return await _context.EntityTypes
            .Include(u => u.Entities)
            .FirstOrDefaultAsync(u => u.Entities.Any(e => e.EntityId == entityId));
    }

    public async Task AddAsync(EntityType entityType)
    {
        await _context.EntityTypes.AddAsync(entityType);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<EntityType>> GetAllAsync(string language)
    {
        var entityTypes = await _context.EntityTypes
            .Include(et => et.Translations)
            .ThenInclude(t => t.Language)
            .Where(et => et.Name != "Public")
            .ToListAsync();

        // Filtrage en mémoire (post-requête)
        foreach (var entityType in entityTypes)
            entityType.Translations = entityType.Translations
                .Where(t => t.Language.Code == language)
                .ToList();

        return entityTypes;
    }
}