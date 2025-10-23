using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Ef.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Ef;

public class PublicOrganizationRepository:IPublicOrganizationRepository
{
    private readonly DoorsDbContext _context;

    public PublicOrganizationRepository(DoorsDbContext context)
    {
        _context = context;
    }

    public async Task<PublicOrganization> AddAsync(PublicOrganization publicOrganization)
    {
        _context.PublicOrganizations.Add(publicOrganization);
        await _context.SaveChangesAsync();
        return publicOrganization;
    }

    public async Task<PublicOrganization?> GetByIdAsync(int publicOrganizationId)
    {
        return await _context.PublicOrganizations
            .FirstOrDefaultAsync(po => po.PublicOrganizationId == publicOrganizationId);
    }

    public async Task<bool> ExistsAsync(int publicOrganizationId)
    {
        return await _context.PublicOrganizations
            .AnyAsync(po => po.PublicOrganizationId == publicOrganizationId);
    }

    public async Task<bool> ExistNameAsync(string name)
    {
        return await _context.PublicOrganizations
            .AnyAsync(po => po.Name.ToLower() == name.ToLower());
    }
}