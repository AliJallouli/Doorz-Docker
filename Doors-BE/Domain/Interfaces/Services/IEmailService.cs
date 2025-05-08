namespace Domain.Interfaces.Services;

public interface IEmailService
{
    Task SendEmailAsync(
        string recipientEmail,
        string subject,
        string templateName,
        Dictionary<string, string> templateData,
        string languageCode,
        string senderEmail,
        string senderName
    );
    Task SendEmailAsync(string email, string subject, string templateName, Dictionary<string, string> templateData);
}
