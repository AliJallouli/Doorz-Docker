using System.Text.Json;
using Application.UseCases.Auth.DTOs;
using Application.UseCases.Auth.DTOs.Password;
using Application.Utils;
using Domain.Constants;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Infrastructure.Ef.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Auth.Service;

public class SecurityTokenService : ISecurityTokenService
{
    private readonly ILogger<SecurityTokenService> _logger;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ISecurityTokenRepository _securityTokenRepository;
    private readonly ITokenService _tokenService;
    private readonly ITokenTypeRepository _tokenTypeRepository;
    private readonly IAuthenticationService _authService;
    private readonly IOtpSendLogRepository _otpSendLogRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SecurityTokenService(
        ISecurityTokenRepository securityTokenRepository,
        ITokenTypeRepository tokenTypeRepository,
        IPasswordHasher passwordHasher,
        ITokenService tokenService,
        ILogger<SecurityTokenService> logger,
        IAuthenticationService authService,
        IOtpSendLogRepository otpSendLogRepository,
        IUnitOfWork unitOfWork)
    {
        _securityTokenRepository = securityTokenRepository ?? throw new ArgumentNullException(nameof(securityTokenRepository), "Le référentiel de jetons de sécurité ne peut pas être null.");
        _tokenTypeRepository = tokenTypeRepository ?? throw new ArgumentNullException(nameof(tokenTypeRepository), "Le référentiel des types de jetons ne peut pas être null.");
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher), "Le service de hachage de mot de passe ne peut pas être null.");
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService), "Le service de jetons ne peut pas être null.");
        _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Le logger ne peut pas être null.");
        _authService = authService ?? throw new ArgumentNullException(nameof(authService), "Le service d'authentification ne peut pas être null.");
        _otpSendLogRepository = otpSendLogRepository ?? throw new ArgumentNullException(nameof(otpSendLogRepository), "Le référentiel des journaux d'envoi d'OTP ne peut pas être null.");
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork), "L'unité de travail ne peut pas être null.");
    }

    public async Task<GeneratedTokenResultDto> ReGenerateAndStoreAsync(
        int userId,
        string tokenTypeName,
        string ipAddress,
        string userAgent,
        string? deviceId = null,
        Dictionary<string, string>? metadata = null)
    {
        _logger.LogInformation(
            "Début de la régénération de jeton pour l'utilisateur {UserId}, type {TokenTypeName}, IP {IpAddress}",
            userId, tokenTypeName, ipAddress);

        try
        {
            // 1. Récupération du type de jeton
            var tokenType = await _tokenTypeRepository.GetByNameAsync(tokenTypeName)
                            ?? throw new BusinessException(ErrorCodes.TokenTypeNotFound, "tokenType");
            _logger.LogDebug("Type de jeton {TokenTypeName} récupéré avec l'ID {TokenTypeId}",
                tokenTypeName, tokenType.TokenTypeId);

            // 2. Vérification du rate limiting si activé
            if (tokenType.IsRateLimited)
            {
                _logger.LogDebug(
                    "Vérification de la limitation de débit pour l'utilisateur {UserId}, type {TokenTypeName}, fenêtre {RateLimitWindowMinutes} minutes",
                    userId, tokenTypeName, tokenType.RateLimitWindowMinutes);
                var minCreatedAt = DateTime.UtcNow.AddMinutes(-tokenType.RateLimitWindowMinutes);
                var recentTokens = (await _securityTokenRepository
                    .GetByUserIdAndTypeWithinWindow(userId, tokenTypeName, minCreatedAt)).ToList();

                if (recentTokens.Count >= tokenType.MaxRequestsPerWindow)
                {
                    _logger.LogWarning(
                        "Limite de débit dépassée pour l'utilisateur {UserId}, type {TokenTypeName}, nombre de jetons {TokenCount}",
                        userId, tokenTypeName, recentTokens.Count);
                    throw new BusinessException(ErrorCodes.TooManyTokenRequests, "token");
                }

                var lastToken = recentTokens.OrderByDescending(t => t.CreatedAt).FirstOrDefault();
                if (lastToken != null &&
                    (DateTime.UtcNow - lastToken.CreatedAt).TotalMinutes < tokenType.MinDelayMinutes)
                {
                    _logger.LogWarning(
                        "Demande de jeton trop rapide pour l'utilisateur {UserId}, type {TokenTypeName}, dernier jeton créé à {LastCreatedAt}",
                        userId, tokenTypeName, lastToken.CreatedAt);
                    throw new BusinessException(ErrorCodes.TooSoonForNewToken, "token");
                }
            }

            // 3. Génération du jeton et/ou du code OTP
            string? rawToken = null;
            string? tokenHash = null;

            if (tokenType.TokenRequired)
            {
                rawToken = _tokenService.GenerateSecurityToken();
                tokenHash = _passwordHasher.Hash(rawToken);
                _logger.LogDebug(
                    "Jeton généré pour l'utilisateur {UserId}, type {TokenTypeName}, jeton (tronqué) : {TokenTruncated}",
                    userId, tokenTypeName, TokenUtils.TruncateToken(rawToken));
            }

            string? rawOtp = null;
            string? codeOtpHash = null;

            if (tokenType.CodeOtpRequired)
            {
                rawOtp = _tokenService.GenerateOtpCode();
                codeOtpHash = _passwordHasher.Hash(rawOtp);
                _logger.LogDebug(
                    "Code OTP généré pour l'utilisateur {UserId}, type {TokenTypeName}, OTP (tronqué) : {OtpTruncated}",
                    userId, tokenTypeName, TokenUtils.TruncateToken(rawOtp));
            }

            // 4. Calcul de l’expiration
            var tokenExpiration = DateTime.UtcNow.AddMinutes(tokenType.DefaultTokenExpirationMinutes);
            var otpExpiration = tokenType.CodeOtpRequired
                ? DateTime.UtcNow.AddMinutes(tokenType.DefaultOtpExpirationMinutes)
                : (DateTime?)null;

            // 5. Création du SecurityToken
            int userAgentId = await _authService.ProcessUserAgentAsync(userAgent);
            if (tokenType.TokenRequired && string.IsNullOrWhiteSpace(tokenHash))
            {
                _logger.LogError(
                    "Échec de la génération du hash de jeton pour l'utilisateur {UserId}, type {TokenTypeName}",
                    userId, tokenTypeName);
                throw new BusinessException(ErrorCodes.TokenGenerationFailed, "TokenHash");
            }

            var token = new SecurityToken
            {
                UserId = userId,
                TokenTypeId = tokenType.TokenTypeId,
                TokenHash = tokenHash!,
                CodeOtpHash = codeOtpHash,
                IpAddress = ipAddress,
                UserAgentId = userAgentId,
                DeviceId = deviceId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                TokenExpiresAt = tokenExpiration,
                OtpExpiresAt = otpExpiration,
                Metadata = metadata != null ? JsonSerializer.Serialize(metadata) : null
            };

            // 6. Révocation des anciens jetons actifs du même type
            _logger.LogInformation("Révocation des jetons précédents pour l'utilisateur {UserId}, type {TokenTypeName}",
                userId, tokenTypeName);
            await RevokeAllForUserAsync(userId, tokenTypeName);

            // 7. Enregistrement
            await _securityTokenRepository.AddAsync(token);
            _logger.LogInformation(
                "Jeton enregistré avec succès pour l'utilisateur {UserId}, type {TokenTypeName}, expire à {TokenExpiresAt}",
                userId, tokenTypeName, tokenExpiration);

            // 8. Retour du(s) code(s) brut(s)
            return new GeneratedTokenResultDto
            {
                RawToken = rawToken,
                CodeOtp = rawOtp
            };
        }
        catch (BusinessException ex)
        {
            _logger.LogError(ex,
                "Échec de la régénération de jeton pour l'utilisateur {UserId}, type {TokenTypeName} : {ErrorCode}",
                userId, tokenTypeName, ex.Key);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex,
                "Erreur inattendue lors de la régénération de jeton pour l'utilisateur {UserId}, type {TokenTypeName}",
                userId, tokenTypeName);
            throw;
        }
    }

    public async Task<GeneratedTokenResultDto> GenerateAndStoreAsync(
        int userId,
        string tokenTypeName,
        string ipAddress,
        string userAgent,
        string? deviceId = null,
        Dictionary<string, string>? metadata = null)
    {
        _logger.LogInformation(
            "Début de la génération de jeton pour l'utilisateur {UserId}, type {TokenTypeName}, IP {IpAddress}",
            userId, tokenTypeName, ipAddress);

        try
        {
            // 1. Récupération du type de jeton
            var tokenType = await _tokenTypeRepository.GetByNameAsync(tokenTypeName)
                            ?? throw new BusinessException(ErrorCodes.TokenTypeNotFound, "tokenType");
            _logger.LogDebug("Type de jeton {TokenTypeName} récupéré avec l'ID {TokenTypeId}",
                tokenTypeName, tokenType.TokenTypeId);

            // 2. Génération du jeton si requis
            string? rawToken = null;
            string? tokenHash = null;

            if (tokenType.TokenRequired)
            {
                rawToken = _tokenService.GenerateSecurityToken();
                tokenHash = _passwordHasher.Hash(rawToken);
                _logger.LogDebug(
                    "Jeton généré pour l'utilisateur {UserId}, type {TokenTypeName}, jeton (tronqué) : {TokenTruncated}",
                    userId, tokenTypeName, TokenUtils.TruncateToken(rawToken));
            }

            // 3. Génération du code OTP si requis
            string? rawOtp = null;
            string? codeOtpHash = null;

            if (tokenType.CodeOtpRequired)
            {
                rawOtp = _tokenService.GenerateOtpCode();
                codeOtpHash = _passwordHasher.Hash(rawOtp);
                _logger.LogDebug(
                    "Code OTP généré pour lvisions l'utilisateur {UserId}, type {TokenTypeName}, OTP (tronqué) : {OtpTruncated}",
                    userId, tokenTypeName, TokenUtils.TruncateToken(rawOtp));
            }

            // 4. Création du jeton sécurisé
            var tokenExpiration = DateTime.UtcNow.AddMinutes(tokenType.DefaultTokenExpirationMinutes);
            var otpExpiration = tokenType.CodeOtpRequired
                ? DateTime.UtcNow.AddMinutes(tokenType.DefaultOtpExpirationMinutes)
                : (DateTime?)null;
            int userAgentId = await _authService.ProcessUserAgentAsync(userAgent);
            if (tokenType.TokenRequired && string.IsNullOrWhiteSpace(tokenHash))
            {
                _logger.LogError(
                    "Échec de la génération du hash de jeton pour l'utilisateur {UserId}, type {TokenTypeName}",
                    userId, tokenTypeName);
                throw new BusinessException(ErrorCodes.TokenGenerationFailed, "TokenHash");
            }

            var token = new SecurityToken
            {
                UserId = userId,
                TokenTypeId = tokenType.TokenTypeId,
                TokenHash = tokenHash!,
                CodeOtpHash = codeOtpHash,
                IpAddress = ipAddress,
                UserAgentId = userAgentId,
                DeviceId = deviceId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                TokenExpiresAt = tokenExpiration,
                OtpExpiresAt = otpExpiration,
                Metadata = metadata != null ? JsonSerializer.Serialize(metadata) : null
            };

            // 5. Enregistrement
            await _securityTokenRepository.AddAsync(token);
            _logger.LogInformation(
                "Jeton enregistré avec succès pour l'utilisateur {UserId}, type {TokenTypeName}, expire à {TokenExpiresAt}",
                userId, tokenTypeName, tokenExpiration);

            // 6. Retour
            return new GeneratedTokenResultDto
            {
                RawToken = rawToken,
                CodeOtp = rawOtp
            };
        }
        catch (BusinessException ex)
        {
            _logger.LogError(ex,
                "Échec de la génération de jeton pour l'utilisateur {UserId}, type {TokenTypeName} : {ErrorCode}",
                userId, tokenTypeName, ex.Key);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex,
                "Erreur inattendue lors de la génération de jeton pour l'utilisateur {UserId}, type {TokenTypeName}",
                userId, tokenTypeName);
            throw;
        }
    }

    public async Task<(string rawOtp, OtpRegenerationMetadata meta)> RegenerateOtpOnlyAsync(
        int userId,
        string tokenTypeName,
        string ipAddress,
        string userAgent,
        string? deviceId = null)
    {
        _logger.LogInformation("→ Requête de régénération d’OTP : User {UserId}, Type {TokenTypeName}, IP {Ip}",
            userId, tokenTypeName, ipAddress);

        try
        {
            var tokenType = await _tokenTypeRepository.GetByNameAsync(tokenTypeName)
                            ?? throw new BusinessException(ErrorCodes.TokenTypeNotFound, "tokenType");

            if (!tokenType.CodeOtpRequired)
                throw new BusinessException(ErrorCodes.OtpNotRequiredForThisTokenType, "tokenType");

            var token = (await _securityTokenRepository.GetActiveByUserIdAndTypeAsync(userId, tokenTypeName))
                        .OrderByDescending(t => t.CreatedAt)
                        .FirstOrDefault()
                        ?? throw new BusinessException(ErrorCodes.TokenNotFound, "token");

            var resendLimit = tokenType.MaxRequestsPerWindow;
            var window = tokenType.RateLimitWindowMinutes;
            var now = DateTime.UtcNow;

            _logger.LogDebug("Token récupéré : ID={TokenId}, ResendCount={ResendCount}, LastSent={LastSent}",
                token.SecurityTokenId, token.ResendCount, token.LastSentAt);

            // Blocage temporaire si trop de resends dans la fenêtre
            if (token.ResendCount >= resendLimit && token.LastSentAt != null)
            {
                if (token.LastSentAt.Value > now.AddMinutes(-window))
                {
                    var secondsRemaining = (int)(token.LastSentAt.Value.AddMinutes(window) - now).TotalSeconds;

                    if (token.OtpAttemptCount >= tokenType.MaxOtpAttempts)
                    {
                        throw new BusinessException(
                            ErrorCodes.TooManyOtpResends,
                            "otp",
                            429,
                            new Dictionary<string, object>
                            {
                                { "remainingResends", 0 },
                                { "otpAttemptsLeft", 0 },
                                { "remainingWaitTimeSeconds", secondsRemaining }
                            }
                        );
                    }

                    _logger.LogWarning("Resend limité atteint, mais des tentatives OTP sont encore possibles. Aucun nouveau code généré.");
                    return (string.Empty, new OtpRegenerationMetadata());
                }

                //  Fenêtre expirée → reset resendCount uniquement
                token.ResendCount = 0;
                token.LastSentAt = null;
                _logger.LogInformation("→ Fenêtre expirée, reset automatique du ResendCount (Token {TokenId})",
                    token.SecurityTokenId);
            }


            // si la fenêtre est dépasser (remettre tout à 0)
            if (token.LastSentAt != null && token.LastSentAt.Value < now.AddMinutes(-window))
            {
                token.OtpAttemptCount = 0;
                token.OtpRevokedAt = null;
                token.ResendCount = 0;
                token.LastSentAt = null;
                token.UpdatedAt = now;
            }


            // Génération OTP
            var rawOtp = _tokenService.GenerateOtpCode();
            token.CodeOtpHash = _passwordHasher.Hash(rawOtp);
            token.ResendCount += 1;
            token.LastSentAt = now;
            token.OtpExpiresAt = now.AddMinutes(tokenType.DefaultOtpExpirationMinutes);

            // Nouveau otp reset du reste!
            token.OtpAttemptCount = 0;
            token.OtpRevokedAt = null;
            token.UpdatedAt = now;
            _logger.LogInformation("→ Déblocage suite à nouveau OTP : OtpAttemptCount repasse à 0");


            // Log
            int userAgentId = await _authService.ProcessUserAgentAsync(userAgent);
            var log = new OtpSendLog
            {
                SecurityTokenId = token.SecurityTokenId,
                UserAgentId = userAgentId,
                IpAddress = ipAddress,
                SentAt = now
            };

            // Transaction
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _securityTokenRepository.UpdateAsync(token);
                await _otpSendLogRepository.AddAsync(log);
                await _unitOfWork.SaveChangesAsync();
                await transaction.CommitAsync();

                _logger.LogInformation("✅ OTP régénéré avec succès pour le user {UserId}, jeton {TokenId}", userId,
                    token.SecurityTokenId);
                var metadata = new OtpRegenerationMetadata
                {
                    RemainingResends = resendLimit - token.ResendCount,
                    OtpAttemptsLeft = tokenType.MaxOtpAttempts,
                    ResendCount = token.ResendCount
                };

                return (rawOtp, metadata);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, " Erreur lors de la transaction OTP pour user {UserId}, token {TokenId}", userId,
                    token.SecurityTokenId);
                throw;
            }
        }
        catch (BusinessException ex)
        {
            _logger.LogError(ex, " BusinessException sur OTP regenerate : {ErrorCode}", ex.Key);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, " Erreur critique dans RegenerateOtpOnlyAsync");
            throw;
        }
    }


    public async Task<(SecurityToken,OtpRegenerationMetadata)> ValidateTokenAsync(string token, string expectedTypeName)
    {
        _logger.LogInformation("Validation de jeton pour le type {TokenTypeName}, jeton (tronqué) : {TokenTruncated}",
            expectedTypeName, TokenUtils.TruncateToken(token));

        try
        {
            var tokenType = await _tokenTypeRepository.GetByNameAsync(expectedTypeName)
                            ?? throw new BusinessException(ErrorCodes.TokenTypeNotFound);
            _logger.LogDebug("Type de jeton {TokenTypeName} récupéré pour validation", expectedTypeName);

            var minCreatedAt = DateTime.UtcNow.AddMinutes(-tokenType.DefaultTokenExpirationMinutes);
            var validTokens =
                await _securityTokenRepository.GetValidTokensByTypeAndCreatedAfterAsync(expectedTypeName, minCreatedAt);

            foreach (var tokenEntity in validTokens)
            {
                if (_passwordHasher.Verify(tokenEntity.TokenHash, token))
                {
                    _logger.LogDebug("Jeton vérifié pour l'utilisateur {UserId}, type {TokenTypeName}",
                        tokenEntity.UserId, expectedTypeName);

                    if (tokenEntity.Used)
                    {
                        _logger.LogWarning("Jeton déjà utilisé pour l'utilisateur {UserId}, type {TokenTypeName}",
                            tokenEntity.UserId, expectedTypeName);
                        throw new BusinessException(ErrorCodes.TokenAlreadyUsed, "token");
                    }

                    if (tokenEntity.Revoked)
                    {
                        _logger.LogWarning(
                            "Jeton révoqué - raison inconnue ou générique - utilisateur {UserId}, type {TokenTypeName}",
                            tokenEntity.UserId, expectedTypeName);
                        throw new BusinessException(ErrorCodes.TokenRevoked, "token");
                    }

                    if (tokenEntity.TokenExpiresAt < DateTime.UtcNow)
                    {
                        _logger.LogWarning(
                            "Jeton expiré pour l'utilisateur {UserId}, type {TokenTypeName}, expiration {TokenExpiresAt}",
                            tokenEntity.UserId, expectedTypeName, tokenEntity.TokenExpiresAt);
                        throw new BusinessException(ErrorCodes.TokenExpired, "token");
                    }


                    var resendLimit = tokenType.MaxRequestsPerWindow;
                    var window = tokenType.RateLimitWindowMinutes;
                    var now = DateTime.UtcNow;

                    // la durée du blockage du renvoie de l'otp n'est pas dépassé
                    if (tokenEntity.ResendCount == resendLimit &&
                        tokenEntity.LastSentAt != null &&
                        tokenEntity.LastSentAt.Value > now.AddMinutes(-window) &&
                        tokenEntity.OtpAttemptCount == tokenType.MaxOtpAttempts)
                    {
                        var secondsRemaining = (int)(tokenEntity.LastSentAt.Value.AddMinutes(window) - now).TotalSeconds;

                        _logger.LogWarning("Token bloqué pour trop de resends + otp.");

                        throw new BusinessException(
                            ErrorCodes.TooManyOtpResends,
                            "otp",
                            429,
                            new Dictionary<string, object>
                            {
                                { "remainingResends", 0 },
                                { "otpAttemptsLeft", 0 },
                                { "remainingWaitTimeSeconds", secondsRemaining }
                            });

                    }


                    // la durée du blockage du renvoie de l'otp est dépassé
                    if (tokenEntity.ResendCount == resendLimit &&
                        tokenEntity.LastSentAt != null &&
                        tokenEntity.LastSentAt.Value < now.AddMinutes(-window) &&
                        tokenEntity.OtpAttemptCount == tokenType.MaxOtpAttempts)
                    {
                        _logger.LogWarning("Token bloqué pour trop de tentatives OTP (OtpRevokedAt non null).");

                        throw new BusinessException(
                            ErrorCodes.OtpTooManyAttempts,
                            "otp",
                            429,
                            new Dictionary<string, object>
                            {
                                { "remainingResends", resendLimit},
                                { "otpAttemptsLeft", tokenType.MaxOtpAttempts },
                                { "remainingWaitTimeSeconds", 
                                    (int)(tokenEntity.LastSentAt.Value.AddMinutes(window) - now).TotalSeconds }
                            });

                    }


                    if (tokenEntity.OtpAttemptCount == tokenType.MaxOtpAttempts)
                    {
                        _logger.LogWarning("Token bloqué pour trop de tentatives OTP (OtpAttemptCount == Max)");

                        throw new BusinessException(
                            ErrorCodes.OtpTooManyAttempts,
                            "otp",
                            429,
                            new Dictionary<string, object>
                            {
                                { "remainingResends", Math.Max(0, resendLimit - tokenEntity.ResendCount) },                                { "otpAttemptsLeft", 0 },
                                { "remainingWaitTimeSeconds", 
                                    tokenEntity.LastSentAt.HasValue
                                        ? (int)(tokenEntity.LastSentAt.Value.AddMinutes(window) - now).TotalSeconds
                                        : 0 }
                            });
                    }



                    _logger.LogInformation("Jeton validé avec succès pour l'utilisateur {UserId}, type {TokenTypeName}",
                        tokenEntity.UserId, expectedTypeName);
                    var metadata = new OtpRegenerationMetadata
                    {
                        RemainingResends = resendLimit - tokenEntity.ResendCount,
                        OtpAttemptsLeft = tokenType.MaxOtpAttempts,
                        ResendCount = tokenEntity.ResendCount
                    };
                    return (tokenEntity,metadata);
                }
            }

            _logger.LogWarning("Jeton invalide pour le type {TokenTypeName}, jeton (tronqué) : {TokenTruncated}",
                expectedTypeName, TokenUtils.TruncateToken(token));
            throw new BusinessException(ErrorCodes.TokenInvalid, "token");
        }
        catch (BusinessException ex)
        {
            _logger.LogError(ex, "Échec de la validation de jeton pour le type {TokenTypeName} : {ErrorCode}",
                expectedTypeName, ex.Key);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Erreur inattendue lors de la validation de jeton pour le type {TokenTypeName}",
                expectedTypeName);
            throw;
        }
    }

    public async Task<(SecurityToken,OtpRegenerationMetadata)> ValidateTokenAndOtpCodeAsync(string rawToken, string otpCode, string tokenTypeName)
    {
        _logger.LogInformation(
            "Validation de jeton et OTP pour l'utilisateur, type {TokenTypeName}, jeton (tronqué) : {TokenTruncated}",
            tokenTypeName, TokenUtils.TruncateToken(rawToken));

        try
        {
            // 1. Validation du token cliquable
            var (token,_) = await ValidateTokenAsync(rawToken, tokenTypeName);
            var tokenType = await _tokenTypeRepository.GetByNameAsync(tokenTypeName)
                            ?? throw new BusinessException(ErrorCodes.TokenTypeNotFound);

            var resendLimit = tokenType.MaxRequestsPerWindow;
            var now = DateTime.UtcNow;


            // 5. Vérification de la validité du code OTP
            var otpIsValid = !string.IsNullOrWhiteSpace(otpCode)
                             && token.CodeOtpHash is not null
                             && _passwordHasher.Verify(token.CodeOtpHash, otpCode);

            if (!otpIsValid)
            {
                token.OtpAttemptCount += 1;
                token.UpdatedAt = now;
                token.TokenType = null!;
                await _securityTokenRepository.UpdateAsync(token);

                var otpAttemptsLeft = tokenType.MaxOtpAttempts - token.OtpAttemptCount;
                var resendRemaining = resendLimit - token.ResendCount;

                // 4. Si on atteint la limite
                if (tokenType.MaxOtpAttempts > 0 && token.OtpAttemptCount == tokenType.MaxOtpAttempts)
                {
                    token.OtpRevokedAt = now;

                    var extraData = new Dictionary<string, object>
                    {
                        { "otpAttemptsLeft", 0 },
                        { "maxOtpAttempts", tokenType.MaxOtpAttempts },
                        { "remainingResends", resendRemaining }
                    };

                    await _securityTokenRepository.UpdateAsync(token);

                    if (token.ResendCount == resendLimit)
                    {
                        _logger.LogWarning("Blocage OTP définitif (resends épuisés) : user {UserId}", token.UserId);
                        throw new BusinessException(
                            ErrorCodes.TooManyOtpResends,
                            "otp",
                            429,
                            extraData
                        );
                    }

                    _logger.LogWarning("Blocage OTP définitif : tentatives épuisées pour user {UserId}", token.UserId);
                    throw new BusinessException(
                        ErrorCodes.OtpTooManyAttempts,
                        "otp",
                        429,
                        extraData
                    );
                }

                _logger.LogWarning("Échec OTP user {UserId}, tentative {Attempt}", token.UserId, token.OtpAttemptCount);
                throw new BusinessException(
                    ErrorCodes.OtpInvalid,
                    "otp",
                    400,
                    new Dictionary<string, object>
                    {
                        { "otpAttemptsLeft", otpAttemptsLeft },
                        { "maxOtpAttempts", tokenType.MaxOtpAttempts },
                        { "remainingResends", resendRemaining }
                    }
                );
            }


            _logger.LogInformation("✅ Jeton et OTP validés pour l'utilisateur {UserId}, type {TokenTypeName}",
                token.UserId, tokenTypeName);
            var metadata = new OtpRegenerationMetadata
            {
                RemainingResends = resendLimit - token.ResendCount,
                OtpAttemptsLeft = tokenType.MaxOtpAttempts,
                ResendCount = token.ResendCount
            };
            return (token,metadata);
        }
        catch (BusinessException ex)
        {
            _logger.LogError(ex, " Échec de validation (OTP + Token) : {ErrorCode}", ex.Key);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, " Erreur critique de validation OTP/Token");
            throw;
        }
    }

    public async Task MarkAsUsedAsync(SecurityToken token)
    {
        _logger.LogInformation("Marquage du jeton comme utilisé pour l'utilisateur {UserId}, type ID {TokenTypeId}",
            token.UserId, token.TokenTypeId);

        try
        {
            token.TokenType = null!;
            token.Used = true;
            token.ConsumedAt = DateTime.UtcNow;
            token.UpdatedAt = DateTime.UtcNow;
            await _securityTokenRepository.UpdateAsync(token);
            _logger.LogDebug("Jeton marqué comme utilisé avec succès pour l'utilisateur {UserId}", token.UserId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Échec du marquage du jeton comme utilisé pour l'utilisateur {UserId}", token.UserId);
            throw;
        }
    }

    public async Task RevokeAllForUserAsync(int userId, string tokenTypeName)
    {
        _logger.LogInformation("Révocation de tous les jetons pour l'utilisateur {UserId}, type {TokenTypeName}",
            userId, tokenTypeName);

        try
        {
            var tokenType = await _tokenTypeRepository.GetByNameAsync(tokenTypeName)
                            ?? throw new BusinessException(ErrorCodes.TokenTypeNotFound, "tokenType");
            _logger.LogDebug("Type de jeton {TokenTypeName} récupéré pour révocation", tokenType.Name);

            var tokensToRevoke = (await _securityTokenRepository.GetActiveByUserIdAndTypeAsync(userId, tokenTypeName))
                .ToList();

            if (!tokensToRevoke.Any())
            {
                _logger.LogDebug("Aucun jeton actif à révoquer pour l'utilisateur {UserId}, type {TokenTypeName}",
                    userId, tokenTypeName);
                return;
            }

            foreach (var token in tokensToRevoke)
            {
                token.Revoked = true;
                token.RevokeReason = TokenRevokeReasons.ReplacedByNewToken;
                token.RevokedAt = DateTime.UtcNow;
                token.UpdatedAt = DateTime.UtcNow;
            }

            await _securityTokenRepository.UpdateRangeAsync(tokensToRevoke);
            _logger.LogInformation(
                "Révocation réussie de {TokenCount} jetons pour l'utilisateur {UserId}, type {TokenTypeName}",
                tokensToRevoke.Count, userId, tokenTypeName);
        }
        catch (BusinessException ex)
        {
            _logger.LogError(ex,
                "Échec de la révocation des jetons pour l'utilisateur {UserId}, type {TokenTypeName} : {ErrorCode}",
                userId, tokenTypeName, ex.Key);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex,
                "Erreur inattendue lors de la révocation des jetons pour l'utilisateur {UserId}, type {TokenTypeName}",
                userId, tokenTypeName);
            throw;
        }
    }

    public async Task<bool> IsTokenValidAsync(string token, string expectedTypeName)
    {
        _logger.LogDebug(
            "Vérification de la validité du jeton pour le type {TokenTypeName}, jeton (tronqué) : {TokenTruncated}",
            expectedTypeName, TokenUtils.TruncateToken(token));

        try
        {
            await ValidateTokenAsync(token, expectedTypeName);
            _logger.LogDebug("Jeton validé avec succès pour le type {TokenTypeName}", expectedTypeName);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Échec de la validation du jeton pour le type {TokenTypeName}", expectedTypeName);
            return false;
        }
    }

    public async Task<IEnumerable<SecurityToken>> GetActiveTokensForUserAsync(int userId, string tokenTypeName)
    {
        _logger.LogInformation("Récupération des jetons actifs pour l'utilisateur {UserId}, type {TokenTypeName}",
            userId, tokenTypeName);

        try
        {
            var tokenType = await _tokenTypeRepository.GetByNameAsync(tokenTypeName)
                            ?? throw new BusinessException(ErrorCodes.TokenTypeNotFound);
            _logger.LogDebug("Type de jeton {TokenTypeName} récupéré pour la récupération des jetons actifs",
                tokenTypeName);

            var tokens = await _securityTokenRepository.GetActiveByUserIdAndTypeAsync(userId, tokenTypeName);
            var activeTokens = tokens.Where(t =>
                    t.TokenTypeId == tokenType.TokenTypeId && !t.Used && !t.Revoked &&
                    t.TokenExpiresAt > DateTime.UtcNow)
                .ToList();

            _logger.LogDebug("Récupéré {TokenCount} jetons actifs pour l'utilisateur {UserId}, type {TokenTypeName}",
                activeTokens.Count, userId, tokenTypeName);
            return activeTokens;
        }
        catch (BusinessException ex)
        {
            _logger.LogError(ex,
                "Échec de la récupération des jetons actifs pour l'utilisateur {UserId}, type {TokenTypeName} : {ErrorCode}",
                userId, tokenTypeName, ex.Key);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex,
                "Erreur inattendue lors de la récupération des jetons actifs pour l'utilisateur {UserId}, type {TokenTypeName}",
                userId, tokenTypeName);
            throw;
        }
    }

    public async Task<SecurityToken?> FindByRawTokenAsync(string rawToken, string tokenTypeName)
    {
        _logger.LogInformation("Recherche de jeton pour le type {TokenTypeName}, jeton (tronqué) : {TokenTruncated}",
            tokenTypeName, TokenUtils.TruncateToken(rawToken));

        try
        {
            var tokens = await _securityTokenRepository.GetAllTokensByTypeAsync(tokenTypeName);

            foreach (var token in tokens)
            {
                if (_passwordHasher.Verify(token.TokenHash, rawToken))
                {
                    _logger.LogDebug("Jeton trouvé pour l'utilisateur {UserId}, type {TokenTypeName}",
                        token.UserId, tokenTypeName);
                    return token;
                }
            }

            _logger.LogDebug("Aucun jeton trouvé pour le type {TokenTypeName}, jeton (tronqué) : {TokenTruncated}",
                tokenTypeName, TokenUtils.TruncateToken(rawToken));
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Échec de la recherche de jeton pour le type {TokenTypeName}", tokenTypeName);
            throw;
        }
    }
}