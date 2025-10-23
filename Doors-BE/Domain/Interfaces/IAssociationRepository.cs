using Domain.Entities;

namespace Domain.Interfaces;

public interface IAssociationRepository
{
    Task<Association> AddAsync(Association association);
    Task<Association?> GetByIdAsync(int associationId);
    Task<bool> ExistsAsync(int associationId);
    Task<bool> ExistNameAsync(string name);
}