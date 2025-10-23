using Application.Configurations;
using Application.UseCases.Auth.DTOs;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Application.UseCases.Auth.Service;

public class RefreshTokenService:IRefreshTokenService
{
    private readonly ILogger<RefreshTokenService> _logger;
    private readonly ITokenService _tokenService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly ISessionEventRepository _sessionEventRepository;
    private readonly ISessionService _sessionService;
    private readonly AuthSettings _authSettings;

    public RefreshTokenService(
        ILogger<RefreshTokenService> logger,
        ITokenService tokenService,
        IRefreshTokenRepository refreshTokenRepository,
        ISessionEventRepository sessionEventRepository,
        ISessionService sessionService,
        IOptions<AuthSettings> authSettings)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Le logger ne peut pas être null.");
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService), "Le service de jetons ne peut pas être null.");
        _refreshTokenRepository = refreshTokenRepository ?? throw new ArgumentNullException(nameof(refreshTokenRepository), "Le référentiel de jetons de rafraîchissement ne peut pas être null.");
        _sessionEventRepository = sessionEventRepository ?? throw new ArgumentNullException(nameof(sessionEventRepository), "Le référentiel des événements de session ne peut pas être null.");
        _sessionService = sessionService ?? throw new ArgumentNullException(nameof(sessionService), "Le service de session ne peut pas être null.");
        _authSettings = authSettings.Value ?? throw new ArgumentNullException(nameof(authSettings), "Les paramètres d'authentification ne peuvent pas être null.");
    }
    public async Task<RefreshTokenResponseDto> CreateNewRefreshTokenTokenAsync(Users user, int sessionEventId, IDbContextTransaction transaction)
    {
         try
        {
            // Vérifier si un refresh token non utilisé existe déjà
            var existingToken = await _refreshTokenRepository.GetValidTokenBySessionAsync(sessionEventId);
            if (existingToken != null)
            {
                bool rememberMe = await _sessionService.IsRememberMe(existingToken.Token);

                _logger.LogInformation("Refresh token non utilisé trouvé pour la session {SessionEventId}, RefreshTokenId {RefreshTokenId}",
                    sessionEventId, existingToken.RefreshTokenId);
                return new RefreshTokenResponseDto
                {
                    AccessToken = _tokenService.GenerateAccessToken(user,sessionEventId),
                    RefreshToken = existingToken.Token,
                    RememberMe = rememberMe,
                    Message = "Refresh success (reuse)"
                };
            }
            
            // Récupérer SessionEvent pour obtenir RememberMe
            var sessionEvent = await _sessionEventRepository.GetByIdAsync(sessionEventId);
            if (sessionEvent == null)
            {
                _logger.LogError("SessionEvent non trouvé pour SessionEventId {SessionEventId}", sessionEventId);
                throw new BusinessException(ErrorCodes.SessionEventNotFound, "sessionEvent");
            }
            await _sessionService.UpdateSessionActivityAsync(sessionEventId);

            var newAccessToken = _tokenService.GenerateAccessToken(user,sessionEvent.SessionEventId);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            var newTokenEntity = new RefreshToken
            {
                UserId = user.UserId,
                SessionEventId = sessionEventId,
                Token = newRefreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(sessionEvent.RememberMe ? _authSettings.LongLivedRefreshTokenDays : _authSettings.ShortLivedRefreshTokenDays),
                CreatedAt = DateTime.UtcNow,
                Used = false
            };
            await _refreshTokenRepository.AddAsync(newTokenEntity);
            _logger.LogInformation(
                "Nouveau refresh token créé pour la session {SessionEventId}, RefreshTokenId {RefreshTokenId}",
                newTokenEntity.SessionEventId, newTokenEntity.RefreshTokenId);

            return new RefreshTokenResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                RememberMe = sessionEvent.RememberMe,
                Message = "Refresh success (reuse)"
            };
        }
        catch (DbUpdateException ex) when (ex.InnerException?.Message.Contains("duplicate key") ?? false)
        {
            _logger.LogWarning("Un refresh token non utilisé existe déjà pour la session {SessionEventId}", sessionEventId);
            throw new BusinessException(ErrorCodes.RefreshTokenAlreadyExists, "session");
        }
    }
    
}