using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserActionLogRepository
{
    Task<(bool CanPerform, long? RemainingSeconds)> CanPerformActionAsync(int userId, string actionType, int maxAttempts, int timeWindowSeconds);
    Task AddAsync(UserActionLog userActionLog);
}