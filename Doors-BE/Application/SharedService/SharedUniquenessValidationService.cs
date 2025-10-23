using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.SharedService;

public class SharedUniquenessValidationService : ISharedUniquenessValidationService
{
    private readonly IUserRepository _userRepository;
    private readonly IEntityRepository _entityRepository;
    private readonly IInvitationRequestRepository _invitationRequestRepository;
    private readonly ISuperadminInvitationRepository _superadminInvitationRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly ILogger<SharedUniquenessValidationService> _logger;

    public SharedUniquenessValidationService(
        IUserRepository userRepository,
        IEntityRepository entityRepository,
        IInvitationRequestRepository invitationRequestRepository,
        ISuperadminInvitationRepository superadminInvitationRepository,
        ICompanyRepository companyRepository,
        ILogger<SharedUniquenessValidationService> logger)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository), "Le référentiel utilisateur ne peut pas être null.");
        _entityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository), "Le référentiel d'entité ne peut pas être null.");
        _invitationRequestRepository = invitationRequestRepository ?? throw new ArgumentNullException(nameof(invitationRequestRepository), "Le référentiel de requêtes d'invitation ne peut pas être null.");
        _superadminInvitationRepository = superadminInvitationRepository ?? throw new ArgumentNullException(nameof(superadminInvitationRepository), "Le référentiel d'invitations de superadmin ne peut pas être null.");
        _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository), "Le référentiel d'entreprise ne peut pas être null.");
        _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Le logger ne peut pas être null.");
    }

    public async Task ValidateUniqueEmailInUsersAsync(string email, string process)
    {
        if (await _userRepository.ExistsByEmailAsync(email))
        {
            _logger.LogWarning("Email '{Email}' already exists in users for process '{Process}'.", email, process);
            throw new BusinessException(ErrorCodes.EmailAlreadyUsed, process);
        }
    }
    
    public async Task ValidateUniqueEmailInSuperAdminInvitationAsync(string email, string process)
    {
        if (await _superadminInvitationRepository.ExistsByEmailAsync(email))
        {
            _logger.LogWarning("Email '{Email}' already exists in superadmin invitations for process '{Process}'.", email, process);
            throw new BusinessException(ErrorCodes.EmailAlreadyUsed, process);
        }
    }
    
    public async Task ValidateUniqueEmailInInvitationRequestsAsync(string email, string process)
    {
        if (await _invitationRequestRepository.ExistEmailAsync(email))
        {
            _logger.LogWarning("Email '{Email}' already exists in invitation requests for process '{Process}'.", email, process);
            throw new BusinessException(ErrorCodes.EmailAlreadyUsed, process);
        }
    }

    public async Task ValidateUniqueEntityNameInEntitiesAsync(string name)
    {
        if (await _entityRepository.ExistNameAsync(name))
        {
            _logger.LogWarning("Entity name '{Name}' already exists in entities.", name);
            throw new BusinessException(ErrorCodes.EntityNameAlreadyUsed, "name");
        }
    }

    public async Task ValidateUniqueEntityNameInInvitationRequestsAsync(string name)
    {
        if (await _invitationRequestRepository.ExistsNameInRequestsAsync(name))
        {
            _logger.LogWarning("Entity name '{Name}' already exists in invitation requests.", name);
            throw new BusinessException(ErrorCodes.EntityNameAlreadyUsed, "name");
        }
    }

    public async Task ValidateUniqueCompanyNumberAsync(string companyNumber)
    {
        if (await _companyRepository.ExistsCompanyNumberAsync(companyNumber)
            || await _invitationRequestRepository.ExistsCompanyNumberInRequestsAsync(companyNumber))
        {
            _logger.LogWarning("Company number '{CompanyNumber}' already exists in companies or invitation requests.", companyNumber);
            throw new BusinessException(ErrorCodes.CompanyNumberAlreadyUsed, "companyNumber");
        }
    }
}