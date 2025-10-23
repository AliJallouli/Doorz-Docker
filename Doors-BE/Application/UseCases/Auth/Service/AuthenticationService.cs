
using Application.Configurations;
using Application.UseCases.Auth.DTOs;
using Application.Utils;


using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Application.UseCases.Auth.Service;

public class AuthenticationService : IAuthenticationService
{
    private readonly ILogger<AuthenticationService> _logger;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;
    private readonly IUserAgentRepository _userAgentRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly AuthSettings _authSettings;
    private readonly ISessionService _sessionService;
    

    public AuthenticationService(
        IUserRepository userRepository,
        ITokenService tokenService,
        ILogger<AuthenticationService> logger,
        IMapper mapper,
        IUserAgentRepository userAgentRepository,
        IRefreshTokenRepository refreshTokenRepository,
        IOptions<AuthSettings> authSettings,
        ISessionService sessionService)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository), "Le référentiel utilisateur ne peut pas être null.");
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService), "Le service de génération de tokens ne peut pas être null.");
        _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Le logger ne peut pas être null.");
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), "Le mapper ne peut pas être null.");
        _userAgentRepository=userAgentRepository ?? throw new ArgumentNullException(nameof(userAgentRepository), "Le userAgentRepository ne peut pas être null.");
        _refreshTokenRepository = refreshTokenRepository ?? throw new ArgumentNullException(nameof(refreshTokenRepository), "Le refreshTokenRepository ne peut pas être null.");
        _authSettings = authSettings.Value ?? throw new ArgumentNullException(nameof(authSettings),"L'authSettings ne peut pas être null.");
        _sessionService = sessionService ?? throw new ArgumentNullException(nameof(sessionService), "Le sessionService ne peut pas être null.");
    }

    public async Task<AuthResponseDto> PerformAutoLoginAsync(Users? user, string ipAddress, string successMessage,
        string userAgent,bool rememberMe, string oppeninReason)
    {
        // 1. ReVérification du user
        if (user == null)
        {
            _logger.LogError("Tentative de connexion automatique avec un utilisateur null");
            throw new BusinessException(ErrorCodes.UserNull, "L'utilisateur est requis pour se connecter.");
        }

        // 2. Enregistrement ou extraction du userAgent
        int userAgentId =await ProcessUserAgentAsync(userAgent);

        
        try
        {
            // 3. Revérification de l'identifiant du user
            if (string.IsNullOrEmpty(user.Email))
                throw new BusinessException(ErrorCodes.MissingEmail, "L'email de l'utilisateur est requis.");
            // Ajout de login attempt = success
            await _userRepository.AddLoginAttemptAsync(new LoginAttempt
            {
                UserId = user.UserId,
                Email = user.Email,
                IpAddress = ipAddress,
                AttemptTime = DateTime.UtcNow,
                UserAgentId = userAgentId,
                Success = true
            });

            // 4. Création de la session
            int sessionId = await _sessionService.CreateSessionAsync(
                user.UserId,
                userAgentId,
                ipAddress,
                rememberMe,
                oppeninReason
            );

            // 5. Mise à jour de la dernière connexion
            await _userRepository.UpdateLastLoginAsync(user.UserId, DateTime.UtcNow);

            // 6. Génération et vérification des tokens: AccessToken et RefreshToken
            string accessToken;
            string refreshToken;
            try
            {
                accessToken = _tokenService.GenerateAccessToken(user,sessionId);
                refreshToken = _tokenService.GenerateRefreshToken();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la génération des tokens pour l'utilisateur {UserId}",
                    user.UserId);
                throw new BusinessException(ErrorCodes.TokenGenerationFailed, "Impossible de générer les tokens.");
            }

            if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken))
            {
                _logger.LogError("Tokens générés invalides ou vides pour l'utilisateur {UserId}", user.UserId);
                throw new BusinessException(ErrorCodes.TokenInvalid, "Les tokens générés sont invalides.");
            }

            // 7. Ajout du refreshToken en db
            await _refreshTokenRepository.AddRefreshTokenAsync(new RefreshToken
            {
                UserId = user.UserId,
                SessionEventId = sessionId,
                Token = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(rememberMe ? _authSettings.LongLivedRefreshTokenDays : _authSettings.ShortLivedRefreshTokenDays),
                CreatedAt = DateTime.UtcNow
            });
            
            return new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                Message = successMessage
            };
        }
        catch (BusinessException ex)
        {
            _logger.LogWarning("Erreur métier dans PerformAutoLoginAsync : {ErrorCode} - {Message}", ex.Key,
                ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur inattendue lors de l'auto-login pour l'utilisateur {UserId}", user.UserId);
            throw new BusinessException(ErrorCodes.UnexpectedError, "Erreur inattendue lors de l'authentification.");
        }
    }
    
    public async Task<int> ProcessUserAgentAsync(string userAgentValue)
    {
        _logger.LogInformation("Traitement du user agent : {UserAgent}", userAgentValue);

        // 1. Parse le userAgent
        var userAgentDto = UserAgentParser.Parse(userAgentValue);
        var newUserAgent = _mapper.Map<UserAgent>(userAgentDto);
        newUserAgent.UserAgentValue = userAgentValue;

        // 2. Vérifier si le UserAgent existe déjà
        var existingUserAgent = await _userAgentRepository.GetAsync(userAgentValue);

        if (existingUserAgent != null)
        {
            _logger.LogDebug("User agent existant trouvé : {UserAgentId}", existingUserAgent.UserAgentId);
            return existingUserAgent.UserAgentId;
        }

        // 3. Si l'usqer agent n'existe pas: Ajouter le nouveau UserAgent
        await _userAgentRepository.AddAsync(newUserAgent);
        _logger.LogInformation("Nouveau user agent ajouté : {UserAgentValue}", newUserAgent.UserAgentValue);
        
        return newUserAgent.UserAgentId; 
    }
}