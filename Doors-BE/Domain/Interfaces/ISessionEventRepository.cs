using Domain.Entities;

namespace Domain.Interfaces;

public interface ISessionEventRepository
{
    Task<SessionEvent?> GetByIdAsync(int sessionEventId);
    Task<bool> GetRememberMeByRefreshTokenAsync(string refreshToken);
    Task<List<SessionEvent>> GetActiveSessionsByUserAsync(int userId);
    public Task<List<SessionEvent>> GetActiveSessionsByUserExceptAsync(int userId, int sessionIdToKeep);
    Task<int> CreateAsync(SessionEvent session);
    Task UpdateLastSeenAsync(int sessionEventId);
    
    Task SaveChangesAsync();
}