using Domain.Entities;

namespace Domain.Interfaces;

public interface ICompanyRepository
{
    Task<Company> AddAsync(Company company);
    Task<Company?> GetByIdAsync(int companyId);
    Task<bool> ExistsAsync(int companyId);
    Task<bool> ExistNameAsync(string name);
    Task<bool> ExistsCompanyNumberAsync(string companyNumber);
}