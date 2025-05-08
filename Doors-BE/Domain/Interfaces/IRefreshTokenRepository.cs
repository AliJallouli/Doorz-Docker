using Domain.Entities;

namespace Domain.Interfaces;

public interface IRefreshTokenRepository
{
    Task DeleteAsync(int entityUserId);
    Task DeleteAllForUserAsync(int userId);
    Task UpdateAsync(RefreshToken token);
    Task<RefreshToken?> GetLatestValidBySessionAsync(int sessionEventId);


    Task AddAsync(RefreshToken newTokenEntity);
    Task<RefreshToken?> GetValidTokenBySessionAsync(int sessionEventId);
    Task<RefreshToken?> GetRefreshTokenAsync(string token);
}