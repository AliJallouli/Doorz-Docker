using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Ef.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Ef;

public class InstitutionRepository : IInstitutionRepository
{
    private readonly DoorsDbContext _context;

    public InstitutionRepository(DoorsDbContext context)
    {
        _context = context;
    }

    public async Task<Institution> AddAsync(Institution institution)
    {
        _context.Institutions.Add(institution);
        await _context.SaveChangesAsync();
        return institution; // Retourne l’entité avec l’ID généré
    }

    public async Task<Institution?> GetByIdAsync(int institutionId)
    {
        return await _context.Institutions
            .FirstOrDefaultAsync(i => i.InstitutionId == institutionId);
    }

    public async Task<bool> ExistsAsync(int institutionId)
    {
        return await _context.Institutions
            .AnyAsync(i => i.InstitutionId == institutionId);
    }

    public async Task<bool> ExistNameAsync(string name)
    {
        return await _context.Institutions
            .AnyAsync(i => i.Name.ToLower() == name.ToLower());
    }
}