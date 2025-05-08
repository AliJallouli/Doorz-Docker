using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Ef.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Ef;

public class OtpSendLogRepository: IOtpSendLogRepository
{
    
        private readonly DoorsDbContext _context;

        public OtpSendLogRepository(DoorsDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(OtpSendLog log)
        {
            await _context.OtpSendLogs.AddAsync(log);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OtpSendLog>> GetByTokenIdAsync(int securityTokenId)
        {
            return await _context.OtpSendLogs
                .Where(l => l.SecurityTokenId == securityTokenId)
                .ToListAsync();
        }
    
}