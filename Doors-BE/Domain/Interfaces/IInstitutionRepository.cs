using Domain.Entities;

namespace Domain.Interfaces;

public interface IInstitutionRepository
{
    Task<Institution> AddAsync(Institution institution);
    Task<Institution?> GetByIdAsync(int id);

    Task<bool> ExistsAsync(int companyId);
    Task<bool> ExistNameAsync(string name);
}