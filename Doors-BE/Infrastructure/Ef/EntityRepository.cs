using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Ef.Data;

namespace Infrastructure.Ef;

public class EntityRepository : IEntityRepository
{
    private readonly DoorsDbContext _context;

    public EntityRepository(DoorsDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Entity> AddAsync(Entity entity)
    {
        await _context.Entities.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(Entity entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity), "L'entité à mettre à jour ne peut pas être null.");

        // Récupère l'entité existante depuis la base de données
        var existingEntity = await _context.Entities.FindAsync(entity.EntityId);
        if (existingEntity == null)
            throw new KeyNotFoundException($"Aucune entité trouvée avec l'ID {entity.EntityId}.");

        // Met à jour les propriétés modifiées en utilisant SetValues
        _context.Entry(existingEntity).CurrentValues.SetValues(entity);

        // Sauvegarde les modifications dans la base de données
        await _context.SaveChangesAsync();
    }
}