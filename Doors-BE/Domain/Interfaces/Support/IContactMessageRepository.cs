using Domain.Entities.Support;

namespace Domain.Interfaces.Support;

public interface IContactMessageRepository
{
    Task AddAsync(ContactMessage message);
    Task<ContactMessage?> GetByIdAsync(int id);
    Task<List<ContactMessage>> GetAllAsync();
}