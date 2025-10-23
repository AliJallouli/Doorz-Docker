using Application.Configurations;
using Application.UseCases.Auth.DTOs;
using Application.UseCases.Auth.Service;
using Application.Validation;
using Domain.Constants;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Infrastructure.Ef.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Application.UseCases.Auth.UseCases.Authentication;


public class LoginUseCase
{
    private readonly IAuthenticationService _authService;
    private readonly ILogger<LoginUseCase> _logger;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly AuthSettings _authSettings;


    public LoginUseCase(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IUnitOfWork unitOfWork,
        IAuthenticationService authService,
        ILogger<LoginUseCase> logger,
        IOptions<AuthSettings> authSettings)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository), "Le référentiel utilisateur ne peut pas être null.");
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher), "Le service de hachage de mot de passe ne peut pas être null.");
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork), "L'unité de travail ne peut pas être null.");
        _authService = authService ?? throw new ArgumentNullException(nameof(authService), "Le service d'authentification ne peut pas être null.");
        _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Le logger ne peut pas être null.");
        _authSettings = authSettings.Value ?? throw new ArgumentNullException(nameof(authSettings),"L'authSettings ne peut pas être null.");
    }


public async Task<AuthResponseDto> ExecuteAsync(LoginRequestDto request, string ipAddress, string userAgent)
{
    _logger.LogInformation(
        "Début de la validation explicite des identifiants pour l'email {Email} depuis l'IP {IpAddress}",
        request.Email, ipAddress);
    
    // 1. Vérification du nombre de tentatives échouées (Email + IP)
    var failedAttempts = await _userRepository.GetFailedLoginAttemptsCountAsync(
        request.Email, 
        ipAddress,
        TimeSpan.FromMinutes(_authSettings.FailedLoginAttemptsWindowMinutes)
    );
    if (failedAttempts >= _authSettings.MaxFailedLoginAttempts)
    {
        _logger.LogWarning(
            "Trop de tentatives échouées ({FailedAttempts}) pour l'email {Email} depuis l'IP {IpAddress}", 
            failedAttempts, request.Email, ipAddress
        );
        throw new BusinessException(ErrorCodes.TooManyFailedAttempts, "email");
    }
    
    _logger.LogInformation("Parsing du userAgent : {UserAgent}", userAgent);
    int userAgentId = await _authService.ProcessUserAgentAsync(userAgent);
 
    // 2 Validation du format l'email et le mot de passe
    if (!CommonFormatValidator.ValidateEmail(request.Email) || !CommonFormatValidator.ValidatePassword(request.Password))
    {
        // Format invalide: Enregistrement de la tentative de connexion : success = false
        await _userRepository.AddLoginAttemptAsync(new LoginAttempt
        {
            Email = request.Email,
            IpAddress = ipAddress,
            AttemptTime = DateTime.UtcNow,
            UserAgentId = userAgentId,
            Success = false,
        });

        await _unitOfWork.SaveChangesAsync();
        
        throw new BusinessException(ErrorCodes.InvalidLogin, "L'email ou le mot de passe invalid.");
    }
       
    
    _logger.LogDebug("Validation du format de l'email et du mot de passe réussie pour {Email}", request.Email);
    
    // 3. Vérification en db des identifiant: mot de passe et email
    var user = await _userRepository.GetByEmailAsync(request.Email);
    var passwordIsValid = user != null && _passwordHasher.Verify(user.PasswordHash, request.Password);
    if (!passwordIsValid)
    {
        _logger.LogWarning("Échec de la connexion pour l'email {Email} : identifiants incorrects", request.Email);

        await using var failTransaction = await _unitOfWork.BeginTransactionAsync();
        try
        {
            // Identifiants  invalides: Enregistrement de la tentative de connexion : success = false
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
            _logger.LogError(ex, "Échec lors de l'enregistrement de la tentative pour {Email}", request.Email);
            throw new BusinessException(ErrorCodes.LoginAttemptSaveFailed);
        }

        throw new BusinessException(ErrorCodes.InvalidLogin, "email");
    }

    _logger.LogDebug("Identifiants (email et mot de passe validés avec succès pour l'utilisateur {UserId}", user?.UserId);

    // 4. Identifiants valides:  Achever la connexion
    await using var successTransaction = await _unitOfWork.BeginTransactionAsync();
    try
    {
        var authResponse = await _authService.PerformAutoLoginAsync(
            user!,
            ipAddress,
            "Connexion réussie.",
            userAgent,
            request.RememberMe,
        SessionOpeningReasons.Login
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
}}