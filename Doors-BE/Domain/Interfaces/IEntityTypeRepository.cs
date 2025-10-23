using Domain.Entities;

namespace Domain.Interfaces;

public interface IEntityTypeRepository
{
    Task<EntityType?> GetByNameAsync(string name);
    Task<EntityType?> GetByIdAsync(int id);
    Task<EntityType?> GetByEntityIdAsync(int entityId);
    Task AddAsync(EntityType entityType);
    Task<IEnumerable<EntityType>> GetAllAsync(string language);
}