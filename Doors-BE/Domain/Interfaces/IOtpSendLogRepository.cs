using Domain.Entities;

namespace Domain.Interfaces;

public interface IOtpSendLogRepository
{
    Task AddAsync(OtpSendLog log);
    Task<IEnumerable<OtpSendLog>> GetByTokenIdAsync(int securityTokenId);
}