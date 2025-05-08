using Application.UseCases.Auth.DTOs;
using Application.UseCases.Auth.Service;
using Application.Validation;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Infrastructure.Ef.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Auth.UseCases.Authentication;


public class LoginUseCase
{
    private readonly IAuthenticationService _authService;
    private readonly ILogger<LoginUseCase> _logger;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;


    public LoginUseCase(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IUnitOfWork unitOfWork,
        IAuthenticationService authService,
        ILogger<LoginUseCase> logger)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository),
            "Le référentiel utilisateur ne peut pas être null.");
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher),
            "Le service de hachage de mot de passe ne peut pas être null.");
        _unitOfWork = unitOfWork ??
                      throw new ArgumentNullException(nameof(unitOfWork), "L'unité de travail ne peut pas être null.");
        _authService = authService ?? throw new ArgumentNullException(nameof(authService),
            "Le service d'authentification ne peut pas être null.");
        _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Le logger ne peut pas être null.");
        
    }


    public async Task<AuthResponseDto> ExecuteAsync(LoginRequestDto request, string ipAddress, string userAgent)
    {
        _logger.LogInformation(
            "Début de la validation explicite des identifiants pour l'email {Email} depuis l'IP {IpAddress}",
            request.Email, userAgent);

        CommonValidator.ValidateEmail(request.Email);

        _logger.LogDebug("Validation de l'email et du mot de passe réussie pour {Email}", request.Email);

        // 1. Vérification du nombre de tentatives échouées
        var failedAttempts =
            await _userRepository.GetFailedLoginAttemptsCountAsync(request.Email, TimeSpan.FromMinutes(15));
        if (failedAttempts >= 5)
        {
            _logger.LogWarning("Trop de tentatives échouées ({FailedAttempts}) pour l'email {Email}", failedAttempts,
                request.Email);
            throw new BusinessException(ErrorCodes.TooManyFailedAttempts, "email");
        }

        // 2. Vérification de l’utilisateur
        var user = await _userRepository.GetByEmailAsync(request.Email);
        var passwordIsValid = user != null && _passwordHasher.Verify(user.PasswordHash, request.Password);
        if (!passwordIsValid)
        {
            _logger.LogWarning("Échec de la connexion pour l'email {Email} : identifiants incorrects", request.Email);
            _logger.LogInformation("Parsing du userAgent : {UserAgent}", userAgent);
            
           int userAgentId =await _authService.ProcessUserAgentAsync(userAgent);

            await using var failTransaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                
                await _userRepository.AddLoginAttemptAsync(new LoginAttempt
                {
                    Email = request.Email,
                    IpAddress = ipAddress,
                    AttemptTime = DateTime.UtcNow,
                    UserAgentId = userAgentId,
                    Success = false
                });

                await _unitOfWork.SaveChangesAsync();
                await failTransaction.CommitAsync();
                _logger.LogDebug("Tentative de connexion échouée enregistrée pour l'email {Email}", request.Email);
            }
            catch (Exception ex)
            {
                await failTransaction.RollbackAsync();
                _logger.LogError(ex, "Échec lors de la finalisation de la connexion pour {UserId}", user?.UserId);
                throw new BusinessException(ErrorCodes.LoginAttemptSaveFailed);
            }

            throw new BusinessException(ErrorCodes.InvalidLogin, "email");
        }

        _logger.LogDebug("Identifiants validés avec succès pour l'utilisateur {UserId}", user?.UserId);

        // 3. Connexion réussie
        await using var successTransaction = await _unitOfWork.BeginTransactionAsync();
        try
        {
            var authResponse = await _authService.PerformAutoLoginAsync(
                user!,
                ipAddress,
                "Connexion réussie.",
                userAgent
            );

            await _unitOfWork.SaveChangesAsync();
            await successTransaction.CommitAsync();

            _logger.LogInformation("Connexion réussie pour l'utilisateur {UserId}", user!.UserId);
            return authResponse;
        }
        catch (BusinessException ex)
        {
            _logger.LogWarning("Erreur métier dans LoginUseCase : {ErrorCode} - {Message}", ex.Key, ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            await successTransaction.RollbackAsync();
            _logger.LogError(ex, "Erreur inattendue dans LoginUseCase pour l'email {Email}", request.Email);
            throw new BusinessException(ErrorCodes.LoginFinalizationFailed);
        }
    }
}