using Domain.Exceptions;
using Domain.Helpers;
using Domain.Interfaces.Services;
using Domain.Messaging.Interfaces;
using Infrastructure.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Invitation.Service;

public class InvitationEmailService : IInvitationEmailService
{
    private readonly IEmailService _emailSender;
    private readonly ILogger<EmailService> _logger;
    private readonly string _frontendBaseUrl;
    private readonly IMailQueuePublisher<SendTemplatedEmailMessage> _mailQueuePublisher;

    public InvitationEmailService(IEmailService emailSender, ILogger<EmailService> logger, IConfiguration config,
        IMailQueuePublisher<SendTemplatedEmailMessage> mailQueuePublisher)
    {
        _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _frontendBaseUrl = config.GetSection("Frontend:BaseUrl").Value?.TrimEnd('/') ?? "https://localhost:4200";
        _mailQueuePublisher = mailQueuePublisher ?? throw new ArgumentNullException(nameof(mailQueuePublisher));
    }

   public async Task SendInvitationEmailAsync(
    string email,
    string token,
    string entityName,
    string entityTypeName,
    string roleName,
    string emailTemplate,
    bool isColleague,
    string languageCode)
{
    _logger.LogInformation("Envoi de l'email d'invitation à {Email} avec template {Template}", email, emailTemplate);

    // Déterminer le chemin en fonction du type d'invitation
    var invitePath = isColleague
        ? $"/register/{entityTypeName.ToLower()}/colleague/invite/"
        : $"/register/{entityTypeName.ToLower()}/invite/";

    // Construire l'URL d'invitation
    var inviteLink = $"{_frontendBaseUrl}{invitePath}?token={Uri.EscapeDataString(token)}";
    _logger.LogDebug("Lien d'invitation généré : {InviteLink}", inviteLink);

    // Traductions dynamiques
    var translations = EmailTemplateHelper.LoadEmailTranslations(emailTemplate, languageCode);
    _logger.LogDebug("Traductions chargées pour {Template} ({LanguageCode}): {Translations}", 
        emailTemplate, languageCode, string.Join(", ", translations.Select(kv => $"{kv.Key}: {kv.Value}")));

    // Pré-remplacer les placeholders dans les traductions
    foreach (var key in translations.Keys.ToList())
    {
        translations[key] = translations[key]
            .Replace("[EntityName]", entityName)
            .Replace("[EntityType]", entityTypeName)
            .Replace("[RoleName]", roleName)
            .Replace("[PlatformName]", "DOORZ");
    }

    // Préparer les données du modèle
    var templateData = new Dictionary<string, string>
    {
        { "InviteLink", inviteLink },
        { "EntityType", entityTypeName },
        { "EntityName", entityName },
        { "RoleName", roleName },
        { "PlatformName", "DOORZ" }
    };

    // Fusionner les traductions avec templateData
    foreach (var translation in translations)
    {
        templateData[translation.Key] = translation.Value; 
    }

    // Logger templateData pour débogage
    _logger.LogDebug("Données du template pour {Template} ({LanguageCode}): {TemplateData}", 
        emailTemplate, languageCode, string.Join(", ", templateData.Select(kv => $"{kv.Key}: {kv.Value}")));

    // Sujet dynamique avec traduction
    var subject = translations.ContainsKey("Subject")
        ? translations["Subject"].Replace("[EntityName]", entityName)
                                .Replace("[EntityType]", entityTypeName)
                                .Replace("[RoleName]", roleName)
                                .Replace("[PlatformName]", "DOORZ")
        : $"Invitation à rejoindre {entityName} ({entityTypeName})";

    // Logger le sujet final
    _logger.LogDebug("Sujet de l'email : {Subject}", subject);

    try
    {
        var queueMessage = new SendTemplatedEmailMessage
        {
            To = email,
            Subject = subject,
            TemplateName = emailTemplate,
            TemplateData = templateData,
            Language = languageCode,
            From = "noreply@doorz.be",
            FromName = "DOORZ"
        };

        await _mailQueuePublisher.PublishAsync(queueMessage);
        _logger.LogInformation("Email d'invitation publié dans RabbitMQ pour {Email}", email);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Échec de l'envoi de l'email d'invitation à {Email}", email);
        throw new BusinessException(ErrorCodes.EmailSendingFailed, "email");
    }
}

}