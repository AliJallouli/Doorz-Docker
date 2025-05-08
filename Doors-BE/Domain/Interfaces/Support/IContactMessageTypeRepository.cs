using Domain.Entities.Support;

namespace Domain.Interfaces.Support;

public interface IContactMessageTypeRepository
{
    Task<List<ContactMessageType>> GetAllAsync(string languageCode);
    Task<ContactMessageType?> GetByIdAsync(int id);
}