using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Ef.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Ef;

public class AssociationRepository:IAssociationRepository
{
    private readonly DoorsDbContext _context;

    public AssociationRepository(DoorsDbContext context)
    {
        _context = context;
    }

    public async Task<Association> AddAsync(Association association)
    {
        _context.Associations.Add(association);
        await _context.SaveChangesAsync();
        return association;
    }

    public async Task<Association?> GetByIdAsync(int associationId)
    {
        return await _context.Associations
            .FirstOrDefaultAsync(c => c.AssociationId == associationId);
    }

    public async Task<bool> ExistsAsync(int associationId)
    {
        return await _context.Associations
            .AnyAsync(c => c.AssociationId == associationId);
    }

    public async Task<bool> ExistNameAsync(string name)
    {
        return await _context.Associations
            .AnyAsync(c => c.Name.ToLower() == name.ToLower());
    }
}