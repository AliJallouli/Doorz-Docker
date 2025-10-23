using Domain.Entities;

namespace Domain.Interfaces;

public interface IRefreshTokenRepository
{
  
    Task DeleteAllForUserAsync(int userId);
    Task UpdateAsync(RefreshToken token);

    Task AddAsync(RefreshToken newTokenEntity);
    Task<RefreshToken?> GetValidTokenBySessionAsync(int sessionEventId);
    Task<RefreshToken?> GetRefreshTokenAsync(string token);
    
    Task AddRefreshTokenAsync(RefreshToken refreshToken);
    Task RemoveRefreshTokensBySessionAsync(int sessionEventId);
    
    Task DeleteRefreshTokensBySessionIdAsync(int sessionId);
    Task DeleteRefreshTokensByUserIdAsync(int userId);
    Task DeleteRefreshTokensByUserIdExceptAsync(int userId, int sessionIdToKeep);
}