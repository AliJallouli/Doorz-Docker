using Application.UseCases.Auth.DTOs;
using Domain.Entities;

namespace Application.UseCases.Auth.Service;

public interface ISessionService
{
    // Gestion de session
    Task<int> CreateSessionAsync(int userId, int userAgentId, string ipAddress, bool rememberMe, string oppeninReason);
    Task RevokeSessionAsync(int sessionId, string reason);
    Task RevokeAllSessionsAsync(int userId, string? reason = null);
    Task<bool> IsRememberMe(string refreshToken);
    Task RevokeAllSessionsExceptAsync(int userId, int exceptSessionId, string? reason = null);
    Task<List<SessionDto>> GetActiveSessionsAsync(int userId);
    

    // Utilitaires
    Task UpdateSessionActivityAsync(int sessionId); // pour gérer last_seen_at
}