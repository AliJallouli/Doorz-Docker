using Domain.Entities;

namespace Domain.Interfaces;

public interface ISecurityTokenRepository
{
    Task<SecurityToken?> GetByTokenAsync(string token);
    Task<IEnumerable<SecurityToken>> GetByUserIdAsync(int userId);
    Task<SecurityToken?> GetLatestActiveByUserIdAndTypeAsync(int userId, string tokenTypeName);
    Task AddAsync(SecurityToken token);
    Task UpdateAsync(SecurityToken token);
    Task RevokeAllForUserAsync(int userId);
    Task<bool> IsTokenValidAsync(string token);
    Task UpdateRangeAsync(IEnumerable<SecurityToken> tokens);
    Task<IEnumerable<SecurityToken>> GetValidTokensByTypeAsync(string tokenTypeName);
    Task<IEnumerable<SecurityToken>> GetActiveByUserIdAndTypeAsync(int userId, string tokenTypeName);

    Task<IEnumerable<SecurityToken>> GetByUserIdAndTypeWithinWindow(int userId, string tokenTypeName,
        DateTime minCreatedAt);

    Task<IEnumerable<SecurityToken>> GetValidTokensByTypeAndCreatedAfterAsync(string tokenTypeName,
        DateTime minCreatedAt);
    Task<IEnumerable<SecurityToken>> GetAllTokensByTypeAsync(string tokenTypeName);

}