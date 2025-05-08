using Domain.Entities.Support;
using Domain.Helpers;
using Domain.Interfaces.Services;
using Domain.Messaging.Interfaces;
using Infrastructure.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Application.UseCases.Support.Services;

public class ContactEmailService : IContactEmailService
{
    private readonly IEmailService _emailService;
    private readonly ILogger<ContactEmailService> _logger;
    private readonly EmailSenderOptions _emailOptions;
    private readonly IMailQueuePublisher<SendTemplatedEmailMessage> _mailQueuePublisher;

    public ContactEmailService(IEmailService emailService, ILogger<ContactEmailService> logger, IOptions<EmailSenderOptions> emailOptions, IMailQueuePublisher<SendTemplatedEmailMessage> mailQueuePublisher)
    {
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _emailOptions = emailOptions.Value ?? throw new ArgumentNullException(nameof(emailOptions));
        _mailQueuePublisher = mailQueuePublisher ?? throw new ArgumentNullException(nameof(mailQueuePublisher));
    }

    private string GetSenderName(Dictionary<string, string> senderNames, string languageCode, string defaultName = "DOORZ")
    {
        if (senderNames.TryGetValue(languageCode, out var translatedName) && !string.IsNullOrEmpty(translatedName))
        {
            return translatedName;
        }

        return defaultName;
    }

    public async Task NotifySupportAsync(ContactMessage message, string languageCode, string userAgent)
    {
        var templateData = EmailTemplateHelper.LoadEmailTranslations("ContactSupportNotificationEmail", languageCode);

        templateData["Utilisateur"] = message.UserId > 0 ? "Oui" :"Non";
        templateData["FullName"] = message.FullName;
        templateData["Email"] = message.Email;
        templateData["MessageSubject"] = message.Subject;
        templateData["Message"] = message.Message;
        templateData["IpAddress"] = message.IpAddress;
        templateData["ReceivedAt"] = message.ReceivedAt.ToString("f");
        templateData["UserAgent"] = userAgent;
        templateData["Phone"] = message.Phone ?? "Non fourni";

        var senderName = GetSenderName(_emailOptions.Contact.SenderName, languageCode);
        var fromAddress = _emailOptions.NoReply.Address;
        var toAddress = _emailOptions.Contact.Address;

        _logger.LogInformation("Données du template pour ContactSupportNotificationEmail ({LanguageCode}): {TemplateData}",
            languageCode, string.Join(", ", templateData.Select(kv => $"{kv.Key}: {kv.Value}")));

        try
        {
            var queueMessage = new SendTemplatedEmailMessage
            {
                To = toAddress,
                Subject = templateData["Subject"],
                TemplateName = "ContactSupportNotificationEmail",
                TemplateData = templateData,
                Language = languageCode,
                From = fromAddress,
                FromName = senderName
            };

            await _mailQueuePublisher.PublishAsync(queueMessage);
            _logger.LogInformation("Message de notification support publié dans la file pour le message ID {Id}", message.ContactMessageId);


        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de l'envoi de l'email au support.");
            throw;
        }
    }

    public async Task AcknowledgeUserAsync(ContactMessage message, string languageCode)
    {
        var templateData = EmailTemplateHelper.LoadEmailTranslations("ContactUserConfirmationEmail", languageCode);

        templateData["FullName"] = message.FullName;
        templateData["PlatformName"] = "DOORZ";
        templateData["ReceivedAt"] = message.ReceivedAt.ToString("f");
        templateData["MessageSubject"] = message.Subject;
        templateData["Message"] = message.Message;

        var senderName = GetSenderName(_emailOptions.NoReply.SenderName, languageCode);
        var fromAddress = _emailOptions.NoReply.Address;

        _logger.LogInformation("Données du template pour ContactUserConfirmationEmail ({LanguageCode}): {TemplateData}",
            languageCode, string.Join(", ", templateData.Select(kv => $"{kv.Key}: {kv.Value}")));

        try
        {
            var queueMessage = new SendTemplatedEmailMessage
            {
                To = message.Email,
                Subject = templateData["Subject"],
                TemplateName = "ContactUserConfirmationEmail",
                TemplateData = templateData,
                Language = languageCode,
                From = fromAddress,
                FromName = senderName
            };

            await _mailQueuePublisher.PublishAsync(queueMessage);
            _logger.LogInformation("Message d'accusé de réception publié dans la file pour {Email}", message.Email);

        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Échec de l'envoi de l'accusé de réception à {Email}", message.Email);
        }
    }
}
