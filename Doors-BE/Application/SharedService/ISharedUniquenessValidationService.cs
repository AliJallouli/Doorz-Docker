namespace Application.SharedService;

public interface ISharedUniquenessValidationService
{
    Task ValidateUniqueEmailInUsersAsync(string email, string process);
    Task ValidateUniqueEmailInSuperAdminInvitationAsync(string email, string process);

    Task ValidateUniqueEmailInInvitationRequestsAsync(string email, string process);
    Task ValidateUniqueEntityNameInEntitiesAsync(string name);
    Task ValidateUniqueEntityNameInInvitationRequestsAsync(string name);
    Task ValidateUniqueCompanyNumberAsync(string companyNumber);
}