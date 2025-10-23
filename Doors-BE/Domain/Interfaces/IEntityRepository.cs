using Domain.Entities;

namespace Domain.Interfaces;

public interface IEntityRepository
{
    Task<Entity> AddAsync(Entity entity);
    Task UpdateAsync(Entity entity);
    Task<bool> ExistNameAsync(string name);

}