using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserRepository
{
    Task<Users?> GetByIdAsync(int id);
    Task<Users?> GetByEmailAsync(string email);
    Task<Users> AddAsync(Users users);
    Task<bool> ExistsByEmailAsync(string email);
    Task UpdateAsync(Users users);
    Task UpdateLastLoginAsync(int userId, DateTime lastLogin);
    Task AddLoginAttemptAsync(LoginAttempt attempt);
    Task<int> GetFailedLoginAttemptsCountAsync(string email,string ipAddress, TimeSpan timeSpan);
    
  
}