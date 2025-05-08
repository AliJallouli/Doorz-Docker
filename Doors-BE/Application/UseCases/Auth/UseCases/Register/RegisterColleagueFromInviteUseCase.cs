using Application.UseCases.Auth.DTOs;
using Application.UseCases.Auth.Service;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Infrastructure.Ef.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Auth.UseCases.Register;


public class RegisterColleagueFromInviteUseCase : RegisterFromInviteUseCaseBase
{
    private readonly ILogger<RegisterColleagueFromInviteUseCase> _logger;

  
    public RegisterColleagueFromInviteUseCase(
        IUserRepository userRepository,
        ISuperadminInvitationRepository invitationRepository,
        ISuperadminInvitationEntityRepository invitationEntityRepository,
        IEntityUserRepository entityUserRepository,
        IPasswordHasher passwordHasher,
        IEntityTypeRepository entityTypeRepository,
        IRoleRepository roleRepository,
        ISuperRoleRepository superRoleRepository,
        IUnitOfWork unitOfWork,
        IAuthenticationService authService,
        ILogger<RegisterColleagueFromInviteUseCase> logger,
        IEmailAuthService emailAuthService,
        ISecurityTokenService securityTokenService,
        ITokenHasher tokenHasher,
        ILegalConsentService legalConsentService)
        : base(userRepository, invitationRepository, invitationEntityRepository, entityUserRepository,
             passwordHasher, entityTypeRepository, roleRepository, superRoleRepository, unitOfWork,
            authService, logger, emailAuthService, securityTokenService, tokenHasher, legalConsentService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Le logger ne peut pas être null.");
    }


    protected override Task ValidateRoleAsync(Role? role, RegisterFromInviteRequestDto request)
    {
        _logger.LogDebug("Validation du rôle pour l'inscription d'un collègue avec RoleId {RoleId}", request.RoleId);
        // Pas de validation spécifique pour le moment, retourne une tâche complétée
        return Task.CompletedTask; // Hérite de la base, mais peut être surchargé si nécessaire
    }


    protected override string GetSuccessMessage(string redirectUrl)
    {
        // Retourne un message personnalisé pour un collègue
        var message = $"Inscription réussie en tant que collègue. Redirection vers {redirectUrl}";
        _logger.LogDebug("Message de succès généré pour l'inscription d'un collègue : {Message}", message);
        return message;
    }


    public override async Task<AuthResponseDto> ExecuteAsync(RegisterFromInviteRequestDto request, string ipAddress,
        string userAgent,string languageCode)
    {
        _logger.LogInformation("Début de l'inscription d'un collègue pour l'email {Email}", request.Email);
        try
        {
            var result = await base.ExecuteAsync(request, ipAddress, userAgent,languageCode);
            _logger.LogInformation("Inscription d'un collègue réussie pour l'email {Email}", request.Email);
            return result;
        }
        catch (BusinessException)
        {
            throw; // laisser le middleware gérer les erreurs métier
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Échec de l'inscription d'un collègue pour l'email {Email}", request.Email);
            throw new BusinessException(ErrorCodes.ColleagueRegistrationFailed);
        }
    }
}