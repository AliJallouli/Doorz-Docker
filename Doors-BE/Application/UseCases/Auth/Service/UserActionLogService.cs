using Application.Configurations;
using Domain.Constants;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Application.UseCases.Auth.Service;

public class UserActionLogService: IUserActionLogService
{
    private readonly IUserActionLogRepository _userActionLogRepository;
    private readonly RateLimitSettings _rateLimitSettings;
    private readonly ILogger<UserActionLogService> _logger;

    public UserActionLogService(
        IUserActionLogRepository userActionLogRepository,
        IOptions<RateLimitSettings> rateLimitSettingsOptions,
        ILogger<UserActionLogService> logger)
    {
        _userActionLogRepository = userActionLogRepository ?? throw new ArgumentNullException(nameof(userActionLogRepository), "Le référentiel des journaux d'actions utilisateur ne peut pas être null.");
        _rateLimitSettings = rateLimitSettingsOptions.Value ?? throw new ArgumentNullException(nameof(rateLimitSettingsOptions), "Les paramètres de limitation de débit ne peuvent pas être null.");
        _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Le logger ne peut pas être null.");
    }

    public async Task<bool> CanPerformActionAsync(int userId, string actionType)
    {
        (int MaxAttempts, int TimeWindowSeconds) config;

        switch (actionType)
        {
            case UserActionTypes.NameUpdate:
                config = (_rateLimitSettings.NameUpdateMaxAttempts, _rateLimitSettings.NameUpdateTimeWindowSeconds);
                break;
            case UserActionTypes.EmailUpdate:
                config = (_rateLimitSettings.EmailUpdateMaxAttempts, _rateLimitSettings.EmailUpdateTimeWindowSeconds);
                break;
            case UserActionTypes.PasswordUpdate:
                config = (_rateLimitSettings.PasswordUpdateMaxAttempts, _rateLimitSettings.PasswordUpdateTimeWindowSeconds);
                break;
            default:
                _logger.LogError("Type d'action inconnu : {ActionType}", actionType);
                throw new InvalidOperationException($"Type d'action non supporté : {actionType}");
        }

        if (config.MaxAttempts <= 0 || config.TimeWindowSeconds <= 0)
        {
            _logger.LogWarning("Configuration invalide pour {ActionType}: MaxAttempts={MaxAttempts}, TimeWindowSeconds={TimeWindowSeconds}",
                actionType, config.MaxAttempts, config.TimeWindowSeconds);
            return true;
        }

        var (canPerform, remainingSeconds) = await _userActionLogRepository.CanPerformActionAsync(userId, actionType, config.MaxAttempts, config.TimeWindowSeconds);

        if (!canPerform)
        {
            _logger.LogWarning("Limite de taux dépassée pour l'utilisateur {UserId}, action {ActionType}. Temps restant : {RemainingSeconds} secondes",
                userId, actionType, remainingSeconds);
            throw new BusinessException(
                key: ErrorCodes.RateLimitExceeded,
                field: null,
                extraData: new { RemainingSeconds = remainingSeconds ?? 0 }
            );
        }


        return true;
    }
    public async Task LogActionAsync(int userId, string actionType,int userAgentId,string ipAddress,string oldValue, string newValue)
    {
        var logEntry = new UserActionLog
        {
            UserId = userId,
            ActionType = actionType,
            UserAgentId = userAgentId,
            OldValue = oldValue,
            NewValue = newValue,
            IpAddress = ipAddress,
            ActionTimestamp = DateTime.UtcNow,
        };
        await _userActionLogRepository.AddAsync(logEntry);
    }
}