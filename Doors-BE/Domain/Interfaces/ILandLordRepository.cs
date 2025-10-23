using Domain.Entities;

namespace Domain.Interfaces;

public interface ILandLordRepository
{
    Task<Landlord> AddAsync(Landlord landlord);
    Task<Landlord?> GetByIdAsync(int id);

    Task<bool> ExistsAsync(int landLordId); 
}