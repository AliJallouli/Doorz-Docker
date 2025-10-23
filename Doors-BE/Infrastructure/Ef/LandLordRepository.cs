using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Ef.Data;

namespace Infrastructure.Ef;

public class LandLordRepository : ILandLordRepository
{
    private readonly DoorsDbContext _context;

    public LandLordRepository(DoorsDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Landlord> AddAsync(Landlord landlord)
    {
        _context.Landlords.Add(landlord);
        await _context.SaveChangesAsync();
        return landlord; 
    }


    public Task<Landlord?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(int landlordId)
    {
        throw new NotImplementedException();
    }
}