using Domain.Entities.Support;
using Domain.Interfaces.Support;
using Infrastructure.Ef.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Ef.Support;

public class ContactMessageTypeRepository:IContactMessageTypeRepository
{
    private readonly DoorsDbContext _context;

    public ContactMessageTypeRepository(DoorsDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<ContactMessageType>> GetAllAsync(string languageCode)
    {
        var types = await _context.ContactMessageTypes
            .Include(t => t.Translations)
            .ThenInclude(tr => tr.Language)
            .ToListAsync();

        foreach (var type in types)
        {
            type.Translations = type.Translations
                .Where(tr => tr.Language.Code == languageCode)
                .ToList();
        }

        return types;
    }

    public async Task<ContactMessageType?> GetByIdAsync(int id)
    {
        return await _context.ContactMessageTypes
            .Include(t => t.Translations)
            .FirstOrDefaultAsync(t => t.ContactMessageTypeId == id);
    }
    
}