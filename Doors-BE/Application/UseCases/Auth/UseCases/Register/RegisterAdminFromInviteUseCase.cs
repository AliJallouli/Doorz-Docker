using Application.UseCases.Auth.DTOs;
using Application.UseCases.Auth.Service;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Infrastructure.Ef.Interfaces;
using Microsoft.Extensions.Logging;
namespace Application.UseCases.Auth.UseCases.Register;

public class RegisterAdminFromInviteUseCase : RegisterFromInviteUseCaseBase
{
    private readonly ILogger<RegisterAdminFromInviteUseCase> _logger;

    
    public RegisterAdminFromInviteUseCase(
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
        ILogger<RegisterAdminFromInviteUseCase> logger,
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
        _logger.LogDebug("Validation du rôle pour l'inscription d'un administrateur avec RoleId {RoleId}",
            request.RoleId);

        if (role == null || role.EntityTypeId != request.EntityTypeId || role.Name != "Admin")
        {
            _logger.LogError(
                "Rôle invalide pour l'inscription d'un administrateur : RoleId {RoleId}, Name {RoleName}, EntityTypeId {EntityTypeId}",
                role?.RoleId, role?.Name, role?.EntityTypeId);

            throw new BusinessException(ErrorCodes.InvalidAdminRole, "RoleId");
        }

        _logger.LogDebug("Rôle 'Admin' validé avec succès pour EntityTypeId {EntityTypeId}", request.EntityTypeId);
        return Task.CompletedTask;
    }


    protected override string GetSuccessMessage(string redirectUrl)
    {
        
        var message = $"Inscription réussie en tant qu'administrateur. Redirection vers {redirectUrl}";
        _logger.LogDebug("Message de succès généré pour l'inscription d'un administrateur : {Message}", message);
        return message;
    }

    public override async Task<AuthResponseDto> ExecuteAsync(RegisterFromInviteRequestDto request, string ipAddress,
        string userAgent,string languageCode)
    {
        _logger.LogInformation("Début de l'inscription d'un administrateur pour l'email {Email}", request.Email);
        try
        {
            var result = await base.ExecuteAsync(request, ipAddress, userAgent,languageCode);
            _logger.LogInformation("Inscription d'un administrateur réussie pour l'email {Email}", request.Email);
            return result;
        }
        catch (BusinessException)
        {
            throw; 
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Échec de l'inscription d'un administrateur pour l'email {Email}", request.Email);
            throw new BusinessException(ErrorCodes.RegistrationFailed);
        }
    }
}