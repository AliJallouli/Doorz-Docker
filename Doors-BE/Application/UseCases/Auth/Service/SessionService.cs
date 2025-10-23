using Application.UseCases.Auth.DTOs;
using AutoMapper;
using Domain.Constants;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Auth.Service;

public class SessionService : ISessionService
{
    private readonly ISessionEventRepository _sessionEventRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<SessionService> _logger;

    public SessionService(
        ISessionEventRepository sessionEventRepository,
        IRefreshTokenRepository refreshTokenRepository, 
        IMapper mapper,
        ILogger<SessionService> logger)
    {
        _sessionEventRepository = sessionEventRepository ?? throw new ArgumentNullException(nameof(sessionEventRepository), "Le référentiel des événements de session ne peut pas être null.");
        _refreshTokenRepository = refreshTokenRepository ?? throw new ArgumentNullException(nameof(refreshTokenRepository), "Le référentiel de jetons de rafraîchissement ne peut pas être null.");
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), "Le service de mappage ne peut pas être null.");
        _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Le logger ne peut pas être null.");
    }

    public async Task<int> CreateSessionAsync(int userId, int userAgentId, string ipAddress, bool rememberMe, string openingReason)
    {
        var session = new SessionEvent
        {
            UserId = userId,
            UserAgentId = userAgentId,
            IpAddress = ipAddress,
            RememberMe = rememberMe,
            IsRevoked = false,
            OpenedAt = DateTime.UtcNow,
            OpeningReason = openingReason
        };

        return await _sessionEventRepository.CreateAsync(session);
    }

    public async Task RevokeSessionAsync(int sessionId, string reason)
    {
        var session = await _sessionEventRepository.GetByIdAsync(sessionId);
        if (session == null)
            throw new BusinessException(ErrorCodes.SessionEventNotFound, "Session inconnue");

        if (session.IsRevoked) return;

        session.IsRevoked = true;
        session.ClosedAt = DateTime.UtcNow;
        session.ClosingReason = reason;

        // Supprimer les refresh tokens associés à cette session
        await _refreshTokenRepository.DeleteRefreshTokensBySessionIdAsync(sessionId);

        await _sessionEventRepository.SaveChangesAsync();
    }

    public async Task RevokeAllSessionsAsync(int userId, string? reason = null)
    {
        var sessions = await _sessionEventRepository.GetActiveSessionsByUserAsync(userId);

        if (sessions.Count == 0)
            throw new BusinessException(ErrorCodes.NoSessionToRevock, "Aucune session trouvée");

        var now = DateTime.UtcNow;
        var closingReason = reason ?? SessionClosingReasons.AdminForcedLogout;

        foreach (var session in sessions)
        {
            session.IsRevoked = true;
            session.ClosedAt = now;
            session.ClosingReason = closingReason;
        }

        // Supprimer tous les refresh tokens pour cet utilisateur
        await _refreshTokenRepository.DeleteRefreshTokensByUserIdAsync(userId);

        await _sessionEventRepository.SaveChangesAsync();
    }

    public async Task<bool> IsRememberMe(string refreshToken)
    {
        // Utiliser IRefreshTokenRepository pour vérifier si le token est valide et lié à une session avec RememberMe
        var token = await _refreshTokenRepository.GetRefreshTokenAsync(refreshToken);
        if (token == null || token.ExpiresAt < DateTime.UtcNow)
            return false;

        var session = await _sessionEventRepository.GetByIdAsync(token.SessionEventId);
        return session != null && !session.IsRevoked && session.RememberMe;
    }

    public async Task RevokeAllSessionsExceptAsync(int userId, int sessionIdToKeep, string? reason = null)
    {
        var sessions = await _sessionEventRepository.GetActiveSessionsByUserExceptAsync(userId, sessionIdToKeep);

        if (sessions.Count == 0)
        {
            _logger.LogInformation("Aucune autre session active à révoquer pour l'utilisateur {UserId}.", userId);
            return;
        }

        var now = DateTime.UtcNow;
        var closingReason = reason ?? SessionClosingReasons.AdminForcedLogout;

        foreach (var session in sessions)
        {
            session.IsRevoked = true;
            session.ClosedAt = now;
            session.ClosingReason = closingReason;
        }

        // Supprimer les refresh tokens pour toutes les sessions sauf celle à garder
        await _refreshTokenRepository.DeleteRefreshTokensByUserIdExceptAsync(userId, sessionIdToKeep);

        await _sessionEventRepository.SaveChangesAsync();
    }

    public async Task<List<SessionDto>> GetActiveSessionsAsync(int userId)
    {
        var sessions = await _sessionEventRepository.GetActiveSessionsByUserAsync(userId);
        return _mapper.Map<List<SessionDto>>(sessions);
    }

    public async Task UpdateSessionActivityAsync(int sessionId)
    {
        await _sessionEventRepository.UpdateLastSeenAsync(sessionId);
    }
}