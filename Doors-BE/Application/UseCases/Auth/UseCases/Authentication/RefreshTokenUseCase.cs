using Microsoft.Extensions.Logging;
using Application.UseCases.Auth.DTOs;
using Application.Utils;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Infrastructure.Ef.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Auth.UseCases.Authentication;

public class RefreshTokenUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RefreshTokenUseCase> _logger;

    public RefreshTokenUseCase(
        IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository,
        ITokenService tokenService,
        IUnitOfWork unitOfWork,
        ILogger<RefreshTokenUseCase> logger)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _refreshTokenRepository = refreshTokenRepository ?? throw new ArgumentNullException(nameof(refreshTokenRepository));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<AuthResponseDto> ExecuteAsync(RefreshRequestDto refreshTokenDto)
    {
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
            // 1. Récupérer le refresh token avec verrouillage pessimiste (timeout de 5 secondes)
            var token = await _refreshTokenRepository.GetRefreshTokenAsync(refreshTokenDto.RefreshToken)
                .TimeoutAfter(TimeSpan.FromSeconds(5));
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

            // 2. Récupérer l'utilisateur
            var user = await _userRepository.GetByIdAsync(token.UserId);
            if (user == null)
            {
                _logger.LogError("Utilisateur non trouvé pour le UserId {UserId}", token.UserId);
                throw new BusinessException(ErrorCodes.UserNotFound, "userId");
            }

            // 3. Marquer le token comme utilisé
            token.Used = true;
            token.UsedAt = DateTime.UtcNow;
            try
            {
                await _refreshTokenRepository.UpdateAsync(token);
                _logger.LogDebug("Refresh token marqué comme utilisé : RefreshTokenId {RefreshTokenId}", token.RefreshTokenId);
            }
            catch (DbUpdateConcurrencyException)
            {
                _logger.LogWarning("Le refresh token {RefreshTokenId} a déjà été marqué comme utilisé par une autre requête", 
                    token.RefreshTokenId);
                throw new BusinessException(ErrorCodes.RefreshTokenAlreadyUsed, "refreshToken");
            }

            // 4. Créer un nouveau refresh token
            var result = await CreateNewTokenAsync(user, token.SessionEventId, transaction);
            await transaction.CommitAsync();
            return result;
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

    private async Task<AuthResponseDto> CreateNewTokenAsync(Users user, int sessionEventId, IDbContextTransaction transaction)
    {
        try
        {
            // Vérifier si un refresh token non utilisé existe déjà
            var existingToken = await _refreshTokenRepository.GetValidTokenBySessionAsync(sessionEventId);
            if (existingToken != null)
            {
                _logger.LogInformation("Refresh token non utilisé trouvé pour la session {SessionEventId}, RefreshTokenId {RefreshTokenId}",
                    sessionEventId, existingToken.RefreshTokenId);
                return new AuthResponseDto
                {
                    AccessToken = _tokenService.GenerateAccessToken(user),
                    RefreshToken = existingToken.Token,
                    Message = "Refresh success (reuse)"
                };
            }

            var newAccessToken = _tokenService.GenerateAccessToken(user);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            var newTokenEntity = new RefreshToken
            {
                UserId = user.UserId,
                SessionEventId = sessionEventId,
                Token = newRefreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(30),
                CreatedAt = DateTime.UtcNow,
                Used = false
            };
            await _refreshTokenRepository.AddAsync(newTokenEntity);
            _logger.LogInformation(
                "Nouveau refresh token créé pour la session {SessionEventId}, RefreshTokenId {RefreshTokenId}",
                newTokenEntity.SessionEventId, newTokenEntity.RefreshTokenId);

            return new AuthResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
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