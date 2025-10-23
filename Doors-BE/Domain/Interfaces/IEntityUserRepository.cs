using Domain.Entities;

namespace Domain.Interfaces;

public interface IEntityUserRepository
{
    Task AddAsync(EntityUser entityUser);
    Task<EntityUser?> GetByIdAsync(int entityUserId);
    Task<EntityUser?> GetByUserAndEntityAsync(int userId, int entityId); 
    Task<EntityUser?> GetByUserIdAsync(int userId);
    Task UpdateAsync(EntityUser entityUser); 
    Task DeleteAsync(int entityUserId); 
}