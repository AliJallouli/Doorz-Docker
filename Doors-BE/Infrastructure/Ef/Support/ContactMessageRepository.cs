using Domain.Entities.Support;
using Domain.Interfaces.Support;
using Infrastructure.Ef.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Ef.Support;

public class ContactMessageRepository:IContactMessageRepository
{
    private readonly DoorsDbContext _context;

    public ContactMessageRepository(DoorsDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(ContactMessage message)
    {
        await _context.ContactMessages.AddAsync(message);
        await _context.SaveChangesAsync();
    }

    public async Task<ContactMessage?> GetByIdAsync(int id)
    {
        return await _context.ContactMessages
            .Include(m => m.Users)
            .Include(m => m.UserAgent)
            .FirstOrDefaultAsync(m => m.ContactMessageId == id);
    }

    public async Task<List<ContactMessage>> GetAllAsync()
    {
        return await _context.ContactMessages
            .Include(m => m.Users)
            .Include(m => m.UserAgent)
            .OrderByDescending(m => m.ReceivedAt)
            .ToListAsync();
    }
}