using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Ef.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Ef;

public class UserAgentRepository : IUserAgentRepository
{
    private readonly DoorsDbContext _context;
    private readonly ILogger<UserAgentRepository> _logger;

    public UserAgentRepository(DoorsDbContext context, ILogger<UserAgentRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    
   
    public async Task<UserAgent?> GetAsync(string userAgentValue)
    {
        var existing = await _context.UserAgents
            .AsNoTracking()
            .FirstOrDefaultAsync(ua => ua.UserAgentValue == userAgentValue);
        
     
        return existing;
    }

    public async Task AddAsync(UserAgent userAgent)
    {
        await _context.UserAgents.AddAsync(userAgent);
        await _context.SaveChangesAsync();
    }


 
}