namespace Application.UseCases.Invitation.Service;

public interface IInvitationEmailService
{
    Task SendInvitationEmailAsync(
        string email,
        string token,
        string entityName,
        string entityTypeName,
        string roleName,
        string emailTemplate,
        bool isCollegue,
        string languageCode 
    );
}