using Microsoft.Extensions.Logging;
using Application.UseCases.Auth.DTOs;
using Application.Utils;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Infrastructure.Ef.Interfaces;
using Application.Configurations;
using Application.UseCases.Auth.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Application.UseCases.Auth.UseCases.Authentication;

public class RefreshTokenUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RefreshTokenUseCase> _logger;
    private readonly AuthSettings _authSettings;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly ISessionService _sessionService;


    public RefreshTokenUseCase(
        IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository,
        ITokenService tokenService,
        IUnitOfWork unitOfWork,
        ILogger<RefreshTokenUseCase> logger,
        IOptions<AuthSettings> authSettings,
        IRefreshTokenService refreshTokenService,
        ISessionService sessionService)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository),
            "Le référentiel utilisateur ne peut pas être null.");
        _refreshTokenRepository = refreshTokenRepository ?? throw new ArgumentNullException(
            nameof(refreshTokenRepository), "Le référentiel de jetons de rafraîchissement ne peut pas être null.");
        _tokenService = tokenService ??
                        throw new ArgumentNullException(nameof(tokenService),
                            "Le service de jetons ne peut pas être null.");
        _unitOfWork = unitOfWork ??
                      throw new ArgumentNullException(nameof(unitOfWork), "L'unité de travail ne peut pas être null.");
        _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Le logger ne peut pas être null.");
        _authSettings = authSettings.Value ?? throw new ArgumentNullException(nameof(authSettings),
            "Les paramètres d'authentification ne peuvent pas être null.");
        _refreshTokenService = refreshTokenService ?? throw new ArgumentNullException(nameof(refreshTokenService),
            "Le service de jetons de rafraîchissement ne peut pas être null.");
        _sessionService = sessionService ??
                          throw new ArgumentNullException(nameof(sessionService),
                              "Le service de session ne peut pas être null.");
    }

    public async Task<RefreshTokenResponseDto> ExecuteAsync(RefreshRequestDto refreshTokenDto)
    {
        // 1. Validation du format du refreshToken
        if (string.IsNullOrWhiteSpace(refreshTokenDto.RefreshToken))
        {
            _logger.LogError("Refresh token manquant ou vide pour le rafraîchissement");
            throw new BusinessException(ErrorCodes.RefreshTokenRequired, "refreshToken");
        }

        _logger.LogInformation(
            "Début du processus de rafraîchissement des tokens pour le refresh token {RefreshToken}",
            TokenUtils.TruncateToken(refreshTokenDto.RefreshToken));

        await using var transaction = await _unitOfWork.BeginTransactionAsync();
        try
        {
            // 2. Récupérer le refresh token avec verrouillage pessimiste (timeout de 5 secondes) si session révoquée le refreshtoken n'existe plus
            var token = await _refreshTokenRepository.GetRefreshTokenAsync(refreshTokenDto.RefreshToken)
                .TimeoutAfter(TimeSpan.FromSeconds(_authSettings.RefreshTokenLockTimeoutSeconds));
            if (token == null)
            {
                _logger.LogWarning("Refresh token non trouvé : {RefreshToken}",
                    TokenUtils.TruncateToken(refreshTokenDto.RefreshToken));
                throw new BusinessException(ErrorCodes.RefreshTokenInvalidOrExpired, "refreshToken");
            }

            if (token.ExpiresAt < DateTime.UtcNow)
            {
                _logger.LogWarning("Refresh token expiré : {RefreshToken}, ExpiresAt: {ExpiresAt}",
                    TokenUtils.TruncateToken(refreshTokenDto.RefreshToken), token.ExpiresAt);
                throw new BusinessException(ErrorCodes.RefreshTokenInvalidOrExpired, "refreshToken");
            }

            if (token.Used)
            {
                _logger.LogWarning("Refresh token déjà utilisé : {RefreshToken}, UsedAt: {UsedAt}",
                    TokenUtils.TruncateToken(refreshTokenDto.RefreshToken), token.UsedAt);
                throw new BusinessException(ErrorCodes.RefreshTokenAlreadyUsed, "refreshToken");
            }

            // 3. Récupérer l'utilisateur
            var user = await _userRepository.GetByIdAsync(token.UserId);
            if (user == null)
            {
                _logger.LogError("Utilisateur non trouvé pour le UserId {UserId}", token.UserId);
                throw new BusinessException(ErrorCodes.UserNotFound, "userId");
            }

            // 4. Rechercher un refresh token existant **valide ET créé il y a moins de X minutes**
            var existingToken = await _refreshTokenRepository.GetValidTokenBySessionAsync(token.SessionEventId);

            if (existingToken != null && (DateTime.UtcNow - existingToken.CreatedAt).TotalMinutes <
                _authSettings.RefreshTokenReuseThresholdMinutes)
            {
                bool rememberMe = await _sessionService.IsRememberMe(existingToken.Token);
                _logger.LogInformation("Token réutilisé (moins de 14 min) pour session {SessionEventId}",
                    token.SessionEventId);
                return new RefreshTokenResponseDto
                {
                    AccessToken = _tokenService.GenerateAccessToken(user, token.SessionEventId),
                    RefreshToken = existingToken.Token,
                    RememberMe = rememberMe,
                    Message = "Refresh success (recent reuse)"
                };
            }

            // 5. Marquer le token reçu comme utilisé
            token.Used = true;
            token.UsedAt = DateTime.UtcNow;
            try
            {
                await _refreshTokenRepository.UpdateAsync(token);
                _logger.LogDebug("Refresh token marqué comme utilisé : RefreshTokenId {RefreshTokenId}",
                    token.RefreshTokenId);
            }
            catch (DbUpdateConcurrencyException)
            {
                _logger.LogWarning(
                    "Le refresh token {RefreshTokenId} a déjà été marqué comme utilisé par une autre requête",
                    token.RefreshTokenId);
                throw new BusinessException(ErrorCodes.RefreshTokenAlreadyUsed, "refreshToken");
            }

            // 6. Créer un nouveau refresh token pour la session
            var refreshTokenResponseDto =
                await _refreshTokenService.CreateNewRefreshTokenTokenAsync(user, token.SessionEventId, transaction);
            await transaction.CommitAsync();
            _logger.LogDebug("Fin avec succès du refreshToken pour le token:  {RefreshTokenId}", token.RefreshTokenId);
            return refreshTokenResponseDto;
        }
        catch (TimeoutException)
        {
            await transaction.RollbackAsync();
            _logger.LogWarning("Timeout lors de l'attente du verrou pour le refresh token {RefreshToken}",
                TokenUtils.TruncateToken(refreshTokenDto.RefreshToken));
            throw new BusinessException(ErrorCodes.RefreshTokenLockTimeout, "refreshToken");
        }
        catch (BusinessException)
        {
            await transaction.RollbackAsync();
            throw;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "Erreur inattendue lors du rafraîchissement du token");
            throw new BusinessException(ErrorCodes.RefreshTokenProcessFailed);
        }
    }
}

// Extension pour le timeout
public static class TaskExtensions
{
    public static async Task<T> TimeoutAfter<T>(this Task<T> task, TimeSpan timeout)
    {
        using var cts = new CancellationTokenSource();
        var delayTask = Task.Delay(timeout, cts.Token);
        var completedTask = await Task.WhenAny(task, delayTask);
        if (completedTask == delayTask)
        {
            throw new TimeoutException("L'opération a dépassé le temps imparti.");
        }

        cts.Cancel();
        return await task;
    }
}