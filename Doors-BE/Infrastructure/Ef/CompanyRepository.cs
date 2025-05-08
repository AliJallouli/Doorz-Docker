using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Ef.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Ef;

public class CompanyRepository : ICompanyRepository
{
    private readonly DoorsDbContext _context;

    public CompanyRepository(DoorsDbContext context)
    {
        _context = context;
    }

    public async Task<Company> AddAsync(Company company)
    {
        _context.Companies.Add(company);
        await _context.SaveChangesAsync();
        return company; // Retourne l’entité avec l’ID généré
    }

    public async Task<Company?> GetByIdAsync(int companyId)
    {
        return await _context.Companies
            .FirstOrDefaultAsync(c => c.CompanyId == companyId);
    }

    public async Task<bool> ExistsAsync(int companyId)
    {
        return await _context.Companies
            .AnyAsync(c => c.CompanyId == companyId);
    }

    public async Task<bool> ExistNameAsync(string name)
    {
        return await _context.Companies
            .AnyAsync(c => c.Name.ToLower() == name.ToLower());
    }
}