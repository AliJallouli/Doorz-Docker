using Domain.Entities;
using Domain.Helpers;
using Domain.Interfaces.Services;
using Domain.Messaging.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Auth.Service;

public class EmailAuthService : IEmailAuthService
{
    private readonly IEmailService _emailService;
    private readonly ILogger<EmailAuthService> _logger;
    private readonly string _frontendBaseUrl;
    private readonly IMailQueuePublisher<SendTemplatedEmailMessage> _mailQueuePublisher;

    public EmailAuthService(
        IEmailService emailService,
        ILogger<EmailAuthService> logger,
        IConfiguration config,
        IMailQueuePublisher<SendTemplatedEmailMessage> mailQueuePublisher)
    {
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _frontendBaseUrl = config.GetSection("Frontend:BaseUrl").Value?.TrimEnd('/') ?? "https://localhost:4200";
        _mailQueuePublisher = mailQueuePublisher ?? throw new ArgumentNullException(nameof(mailQueuePublisher));
    }

public async Task SendConfirmationEmailLinkAsync(Users user, string confirmationToken, string codeOtp, string languageCode)
{
    var encodedToken = Uri.EscapeDataString(confirmationToken);
    var confirmationLink = $"{_frontendBaseUrl}/confirm-email?token={encodedToken}";

    _logger.LogDebug("Lien de confirmation généré : {ConfirmationLink}", confirmationLink);

    // 1️⃣ Données dynamiques
    var templateData = new Dictionary<string, string>
    {
        { "FirstName", user.FirstName ?? "Utilisateur" },
        { "LastName", user.LastName ?? "" },
        { "PlatformName", "DOORZ" },
        { "ValidationLink", confirmationLink },
        { "OtpCode", codeOtp }
    };

    // 2️⃣ Charger et pré-remplacer les traductions
    var translations = EmailTemplateHelper.LoadEmailTranslations("ConfirmationEmail", languageCode);

    foreach (var key in translations.Keys.ToList())
    {
        translations[key] = ReplacePlaceholders(translations[key], templateData);
    }

    // 3️⃣ Fusion des traductions dans templateData
    foreach (var translation in translations)
    {
        templateData[translation.Key] = translation.Value;
    }

    // 4️⃣ Sujet traduit
    var subject = translations.ContainsKey("Subject") ? translations["Subject"] : translations["EmailTitle"];

    _logger.LogDebug("Données du template pour ConfirmationEmail ({LanguageCode}): {TemplateData}", 
        languageCode, string.Join(", ", templateData.Select(kv => $"{kv.Key}: {kv.Value}")));

    try
    {
        await _mailQueuePublisher.PublishAsync(new SendTemplatedEmailMessage
        {
            To = user.Email,
            Subject = subject,
            TemplateName = "ConfirmationEmail",
            TemplateData = templateData,
            Language = languageCode,
            From = "noreply@doorz.be",
            FromName = "DOORZ"
        });

        _logger.LogInformation("Email de confirmation publié dans la file pour {Email}", user.Email);
    }
    catch (Exception ex)
    {
        _logger.LogWarning(ex, "Échec de l'envoi de l'email de confirmation à {Email}", user.Email);
        throw;
    }
}

public async Task SendOtpCodeEmailAsync(Users user, string codeOtp, string languageCode)
{
    // Charger les traductions
    var translations = EmailTemplateHelper.LoadEmailTranslations("ResentOtpEmail", languageCode);

    // Créer templateData avec les données dynamiques
    var templateData = new Dictionary<string, string>
    {
        { "FirstName", user.FirstName ?? "Utilisateur" },
        { "LastName", user.LastName ?? "" },
        { "PlatformName", "DOORZ" },
        { "OtpCode", codeOtp }
    };

    // Fusionner les traductions avec templateData
    foreach (var translation in translations)
    {
        if (!templateData.ContainsKey(translation.Key))
        {
            templateData[translation.Key] = translation.Value;
        }
    }

    // Logger templateData pour débogage
    _logger.LogDebug("Données du template pour ResentOtpEmail ({LanguageCode}): {TemplateData}", 
        languageCode, string.Join(", ", templateData.Select(kv => $"{kv.Key}: {kv.Value}")));

    var subject = translations["Subject"]; // Utiliser Subject au lieu de EmailTitle pour le sujet

    // Remplacer les variables dans le sujet
    foreach (var data in templateData)
    {
        subject = subject.Replace($"[{data.Key}]", data.Value );
    }

    try
    {
        await _mailQueuePublisher.PublishAsync(new SendTemplatedEmailMessage
        {
            To = user.Email,
            Subject = subject,
            TemplateName = "ResentOtpEmail",
            TemplateData = templateData,
            Language = languageCode,
            From = "noreply@doorz.be",
            FromName = "DOORZ"
        });

        _logger.LogInformation("Email OTP publié dans la file pour {Email}", user.Email);
    }
    catch (Exception ex)
    {
        _logger.LogWarning(ex, "Échec de l'envoi de l'email du nouveau code OTP à {Email}", user.Email);
        throw;
    }
}

public async Task SendPasswordReseLinkEmailAsync(Users user, string token, string otpCode, string ipAddress, string userAgent, string languageCode)
{
    var encodedToken = Uri.EscapeDataString(token);
    var resetLink = $"{_frontendBaseUrl}/reset-password?token={encodedToken}";

    _logger.LogDebug("Lien de réinitialisation généré : {ResetLink}", resetLink);

    // Données dynamiques
    var baseData = new Dictionary<string, string>
    {
        { "FirstName", user.FirstName ?? "Utilisateur" },
        { "ResetLink", resetLink },
        { "OtpCode", otpCode },
        { "PlatformName", "DOORZ" },
        { "IpAddress", ipAddress },
        { "UserAgent", userAgent }
    };

    // Charger le template
    var translations = EmailTemplateHelper.LoadEmailTranslations("ResetPasswordEmail", languageCode);

    // Fusionner et remplacer les placeholders
    foreach (var key in baseData)
    {
        translations[key.Key] = key.Value;
    }

    var finalData = translations.ToDictionary(
        kvp => kvp.Key,
        kvp => ReplacePlaceholders(kvp.Value, baseData)
    );

    // Sujet avec remplacement
    var subject = finalData.ContainsKey("Subject") ? finalData["Subject"] : "Réinitialisation de mot de passe";

    try
    {
        await _mailQueuePublisher.PublishAsync(new SendTemplatedEmailMessage
        {
            To = user.Email,
            Subject = subject,
            TemplateName = "ResetPasswordEmail",
            TemplateData = finalData,
            Language = languageCode,
            From = "noreply@doorz.be",
            FromName = "DOORZ"
        });

        _logger.LogInformation("Email de réinitialisation publié dans la file pour {Email}", user.Email);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Échec de l'envoi de l'email de réinitialisation à {Email}", user.Email);
        throw;
    }
}
public async Task SendPasswordChangedConfirmationEmailAsync(Users user, string ipAddress, string userAgent, string languageCode)
{
    // Charger les traductions
    var translations = EmailTemplateHelper.LoadEmailTranslations("PasswordChangedConfirmation", languageCode);

    // Créer templateData avec les données dynamiques
    var templateData = new Dictionary<string, string>
    {
        { "FirstName", user.FirstName ?? "Utilisateur" },
        { "PlatformName", "DOORZ" },
        { "IpAddress", ipAddress },
        { "UserAgent", userAgent }
    };

    foreach (var key in translations.Keys.ToList())
    {
        translations[key] = ReplacePlaceholders(translations[key], templateData);
    }

    // 3️⃣ Fusion des traductions dans templateData
    foreach (var translation in translations)
    {
        templateData[translation.Key] = translation.Value;
    }

    // Logger templateData pour débogage
    _logger.LogDebug("Données du template pour PasswordChangedConfirmation ({LanguageCode}): {TemplateData}", 
        languageCode, string.Join(", ", templateData.Select(kv => $"{kv.Key}: {kv.Value}")));

    var subject = translations["Subject"];

    // Remplacer les variables dans le sujet
    foreach (var data in templateData)
    {
        subject = subject.Replace($"[{data.Key}]", data.Value );
    }

    try
    {
        var message = new SendTemplatedEmailMessage
        {
            To = user.Email,
            Subject = subject,
            TemplateName = "PasswordChangedConfirmation",
            TemplateData = templateData,
            Language = languageCode,
            From = "noreply@doorz.be",
            FromName = "DOORZ"
        };

        await _mailQueuePublisher.PublishAsync(message);
        _logger.LogInformation("Message de confirmation de changement de mot de passe publié dans la file pour {Email}", user.Email);

    }
    catch (Exception ex)
    {
        _logger.LogWarning(ex, "Échec de l'envoi de l'email de changement de mot de passe à {Email}", user.Email);
        // Non bloquant : on log mais on continue
    }
}
private string ReplacePlaceholders(string text, Dictionary<string, string> data)
{
    foreach (var kv in data)
    {
        text = text.Replace($"[{kv.Key}]", kv.Value);
    }
    return text;
}
}

