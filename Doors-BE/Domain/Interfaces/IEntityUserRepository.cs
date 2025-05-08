using Domain.Entities;

namespace Domain.Interfaces;

public interface IEntityUserRepository
{
    Task AddAsync(EntityUser entityUser); // Ajoute une nouvelle association
    Task<EntityUser?> GetByIdAsync(int entityUserId); // Récupère une association par son ID
    Task<EntityUser?> GetByUserAndEntityAsync(int userId, int entityId); // Récupère une association spécifique
    Task<EntityUser> GetByUserIdAsync(int userId); // Récupère toutes les associations d’un utilisateur
    Task UpdateAsync(EntityUser entityUser); // Met à jour une association existante
    Task DeleteAsync(int entityUserId); // Supprime une association
}