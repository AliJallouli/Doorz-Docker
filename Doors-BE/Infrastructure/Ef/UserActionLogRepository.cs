using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Ef.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Ef;

public class UserActionLogRepository:IUserActionLogRepository
{
    private readonly DoorsDbContext _context;
    private readonly ILogger<UserActionLogRepository> _logger;

    public UserActionLogRepository(DoorsDbContext context, ILogger<UserActionLogRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<(bool CanPerform, long? RemainingSeconds)> CanPerformActionAsync(int userId, string actionType, int maxAttempts, int timeWindowSeconds)
    {
        if (maxAttempts <= 0 || timeWindowSeconds <= 0)
        {
            _logger.LogWarning("Paramètres de limitation invalides pour {ActionType}: MaxAttempts={MaxAttempts}, TimeWindowSeconds={TimeWindowSeconds}",
                actionType, maxAttempts, timeWindowSeconds);
            return (true, null);
        }

        var windowStart = DateTime.UtcNow.AddSeconds(-timeWindowSeconds);
        var recentActions = await _context.UserActionLogs
            .Where(log => log.UserId == userId
                          && log.ActionType == actionType
                          && log.ActionTimestamp >= windowStart)
            .OrderByDescending(log => log.ActionTimestamp)
            .Take(maxAttempts)
            .Select(log => log.ActionTimestamp)
            .ToListAsync();

        var attemptCount = recentActions.Count;

        _logger.LogDebug("Utilisateur {UserId} a effectué {AttemptCount}/{MaxAttempts} actions réussies de type {ActionType} dans la fenêtre de {TimeWindowSeconds} secondes",
            userId, attemptCount, maxAttempts, actionType, timeWindowSeconds);

        if (attemptCount < maxAttempts)
        {
            return (true, null);
        }

        // Calculer le temps restant avant l'expiration de la plus ancienne action
        var oldestActionTimestamp = recentActions.Last(); 
        var timeElapsedSinceOldest = (DateTime.UtcNow - oldestActionTimestamp).TotalSeconds;
        var remainingSeconds = (long)(timeWindowSeconds - timeElapsedSinceOldest);

        return (false, remainingSeconds > 0 ? remainingSeconds : 0);
    }
    public async Task AddAsync(UserActionLog userActionLog)
    {
        _context.UserActionLogs.Add(userActionLog);
        await _context.SaveChangesAsync();
    }
}