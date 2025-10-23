using Application.UseCases.Auth.Service;
using Application.Utils;
using Domain.Constants;
using Domain.Exceptions;
using Domain.Interfaces;
using Infrastructure.Ef.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Auth.UseCases.Authentication;

public class LogoutUseCase
{
    private readonly ILogger<LogoutUseCase> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly ISessionService _sessionService;

    public LogoutUseCase(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        ILogger<LogoutUseCase> logger,
        IRefreshTokenRepository refreshTokenRepository,
        ISessionService sessionService)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository),
            "Le référentiel utilisateur ne peut pas être null.");
        _unitOfWork = unitOfWork ??
                      throw new ArgumentNullException(nameof(unitOfWork), "L'unité de travail ne peut pas être null.");
        _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Le logger ne peut pas être null.");
        _refreshTokenRepository = refreshTokenRepository ??
                                  throw new ArgumentNullException(nameof(refreshTokenRepository),
                                      "Le service refreshtokenRepositoryne peut pas être null.");
        _sessionService = sessionService ??
                          throw new ArgumentNullException(nameof(sessionService),
                              "Le service de session ne peut pas être null.");
    }

    public async Task<string> ExecuteAsync(string refreshToken, string ipAddress, string userAgent)
    {
        // 1. Vérification du format refreshToken
        if (string.IsNullOrWhiteSpace(refreshToken))
        {
            _logger.LogError("Refresh token manquant ou vide pour la déconnexion");
            throw new BusinessException(ErrorCodes.RefreshTokenRequired, "refreshToken");
        }

        _logger.LogInformation("Début de la déconnexion depuis l'IP {IpAddress} avec le refresh token {RefreshToken}",
            ipAddress,
            TokenUtils.TruncateToken(refreshToken));


        await using var transaction = await _unitOfWork.BeginTransactionAsync();
        try
        {
            // 2. Récupère le refresh token avec verrouillage
            var tokenEntity = await _refreshTokenRepository.GetRefreshTokenAsync(refreshToken);
            if (tokenEntity == null)
            {
                _logger.LogWarning("Refresh token invalide ou non trouvé : {RefreshToken}",
                    TokenUtils.TruncateToken(refreshToken));
                throw new BusinessException(ErrorCodes.RefreshTokenInvalid, "refreshToken");
            }

            _logger.LogDebug("Refresh token récupéré avec ID {RefreshTokenId} pour UserId {UserId}",
                tokenEntity.RefreshTokenId, tokenEntity.UserId);

            var userId = tokenEntity.UserId;
            var sessionEventId = tokenEntity.SessionEventId;

            // 3. Vérifie l'existance de l'utilisateur
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning("Utilisateur introuvable pour UserId {UserId}", userId);
                throw new BusinessException(ErrorCodes.UserNotFound, "userId");
            }

            _logger.LogDebug("Utilisateur récupéré : UserId {UserId}, Email {Email}", user.UserId, user.Email);

            // 4. Supprimer tous les refresh tokens pour la session
            try
            {
                await _refreshTokenRepository.RemoveRefreshTokensBySessionAsync(sessionEventId);
                _logger.LogDebug("Tous les refresh tokens supprimés pour la session {SessionEventId}", sessionEventId);
            }
            catch (DbUpdateConcurrencyException)
            {
                _logger.LogWarning(
                    "Conflit de concurrence lors de la suppression des refresh tokens pour la session {SessionEventId}",
                    sessionEventId);
                throw new BusinessException(ErrorCodes.LogoutFailed, "concurrency");
            }

            // 5. Révoquer la session
            await _sessionService.RevokeSessionAsync(sessionEventId, SessionClosingReasons.Logout);
            _logger.LogDebug("Session {SessionEventId} révoquée pour UserId {UserId}", sessionEventId, userId);

            //TODO Trouver une solution pour Invalider l'AccessToken

            await _unitOfWork.SaveChangesAsync();
            await transaction.CommitAsync();

            _logger.LogInformation("Déconnexion réussie pour l'utilisateur {UserId} et la session {SessionEventId}",
                userId, sessionEventId);
            return "Déconnexion réussie.";
        }
        catch (BusinessException ex)
        {
            await transaction.RollbackAsync();
            _logger.LogWarning("Erreur métier pendant le logout : {ErrorCode} - {Message}", ex.Key, ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "Échec de la déconnexion pour le refresh token {RefreshToken}",
                refreshToken.Substring(0, Math.Min(8, refreshToken.Length)) + "...");
            throw new BusinessException(ErrorCodes.LogoutFailed);
        }
    }
}