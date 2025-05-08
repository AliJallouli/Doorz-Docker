using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserRepository
{
    Task<Users?> GetByIdAsync(int id);
    Task<Users?> GetByEmailAsync(string email);
    Task<Users> AddAsync(Users users);
    Task<bool> ExistsByEmailAsync(string email);
    Task UpdateAsync(Users users);
    Task DeleteAsync(int id);
    Task<RefreshToken?> GetRefreshTokenAsync(string token);
    Task AddRefreshTokenAsync(RefreshToken refreshToken);
    Task UpdateLastLoginAsync(int userId, DateTime lastLogin);
    Task AddLoginAttemptAsync(LoginAttempt attempt);
    Task<int> GetFailedLoginAttemptsCountAsync(string email, TimeSpan timeSpan);
    Task<bool> RemoveRefreshTokenAsync(int userId, string refreshToken);
    Task RemoveAllRefreshTokensAsync(int userId);
    Task AddSessionEventAsync(SessionEvent sessionEvent);
    Task RemoveRefreshTokensBySessionAsync(int sessionEventId);
}