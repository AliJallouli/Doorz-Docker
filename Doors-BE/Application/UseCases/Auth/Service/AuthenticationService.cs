
using Application.UseCases.Auth.DTOs;
using Application.Utils;


using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Auth.Service;

public class AuthenticationService : IAuthenticationService
{
    private readonly ILogger<AuthenticationService> _logger;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;
    private readonly IUserAgentRepository _userAgentRepository;

    public AuthenticationService(
        IUserRepository userRepository,
        ITokenService tokenService,
        ILogger<AuthenticationService> logger,
        IMapper mapper,IUserAgentRepository userAgentRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository),
            "Le référentiel utilisateur ne peut pas être null.");
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService),
            "Le service de génération de tokens ne peut pas être null.");
        _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Le logger ne peut pas être null.");
        _mapper = mapper;
        _userAgentRepository=userAgentRepository;
    }

    public async Task<AuthResponseDto> PerformAutoLoginAsync(Users? user, string ipAddress, string successMessage,
        string userAgent)
    {
        if (user == null)
        {
            _logger.LogError("Tentative de connexion automatique avec un utilisateur null");
            throw new BusinessException(ErrorCodes.UserNull, "L'utilisateur est requis pour se connecter.");
        }

        int userAgentId =await ProcessUserAgentAsync(userAgent);


        try
        {
            if (string.IsNullOrEmpty(user.Email))
                throw new BusinessException(ErrorCodes.MissingEmail, "L'email de l'utilisateur est requis.");

            await _userRepository.AddLoginAttemptAsync(new LoginAttempt
            {
                UserId = user.UserId,
                Email = user.Email,
                IpAddress = ipAddress,
                AttemptTime = DateTime.UtcNow,
                UserAgentId = userAgentId,
                Success = true
            });

            var sessionEvent = new SessionEvent
            {
                UserId = user.UserId,
                EventType = "Login",
                IpAddress = ipAddress,
                UserAgentId = userAgentId,
                EventTime = DateTime.UtcNow
            };
            await _userRepository.AddSessionEventAsync(sessionEvent);

            await _userRepository.UpdateLastLoginAsync(user.UserId, DateTime.UtcNow);

            string accessToken;
            string refreshToken;
            try
            {
                accessToken = _tokenService.GenerateAccessToken(user);
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

            await _userRepository.AddRefreshTokenAsync(new RefreshToken
            {
                UserId = user.UserId,
                SessionEventId = sessionEvent.SessionEventId,
                Token = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(30),
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
    
    public async Task<int> ProcessUserAgentAsync(string userAgent)
    {
        // Parse le userAgent
        var userAgentDto = UserAgentParser.Parse(userAgent);
    
        // Mapper le DTO en UserAgent
        var newUserAgent = _mapper.Map<UserAgent>(userAgentDto);
    
        // Attribuer la valeur complète de l'userAgent
        newUserAgent.UserAgentValue = userAgent;

        // Vérifier si le UserAgent existe déjà
        var existingUserAgent = await _userAgentRepository.GetAsync(userAgent);
    
        int userAgentId;
    
        if (existingUserAgent == null)
        {
            // Si l'UserAgent n'existe pas, on l'ajoute
            await _userAgentRepository.AddAsync(newUserAgent);
            userAgentId = newUserAgent.UserAgentId;
        }
        else
        {
            // Si l'UserAgent existe, on récupère son ID
            userAgentId = existingUserAgent.UserAgentId;
        }

        // Retourner l'ID du UserAgent
        return userAgentId;
    }

}