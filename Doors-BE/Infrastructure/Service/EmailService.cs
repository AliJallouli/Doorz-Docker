using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Services;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Infrastructure.Service;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;
    private readonly string _templatesPath;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IOptions<EmailSettings> emailSettingsOptions, IWebHostEnvironment env,
        ILogger<EmailService> logger)
    {
        _emailSettings = emailSettingsOptions.Value ?? throw new ArgumentNullException(nameof(emailSettingsOptions));
        _templatesPath = Path.Combine(env.ContentRootPath, "EmailTemplates");
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task SendEmailAsync(
        string recipientEmail,
        string subject,
        string templateName,
        Dictionary<string, string> templateData,
        string languageCode,
        string senderEmail,
        string senderName)
    {
        try
        {
            var templatePath = Path.Combine(_templatesPath, $"{templateName}.html");
            if (!File.Exists(templatePath))
            {
                _logger.LogError("Template introuvable : {TemplatePath}", templatePath);
                throw new BusinessException(ErrorCodes.EMAIL_SENDING_FAILED, "Template non trouvé");
            }

            var htmlBody = await File.ReadAllTextAsync(templatePath);
            foreach (var data in templateData)
            {
                htmlBody = htmlBody.Replace($"[{data.Key}]", data.Value ?? "");
                subject = subject.Replace($"[{data.Key}]", data.Value ?? "");
            }

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(senderName ?? _emailSettings.SenderName ?? "Application",
                senderEmail ?? _emailSettings.SenderEmail ?? "noreply@example.com"));
            emailMessage.To.Add(new MailboxAddress("", recipientEmail));
            emailMessage.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = htmlBody };
            emailMessage.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort,
                _emailSettings.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.None);
            await client.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);

            _logger.LogInformation("Email envoyé à {RecipientEmail} avec le sujet '{Subject}'", recipientEmail, subject);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de l'envoi de l'email à {RecipientEmail}: {Message}", recipientEmail, ex.Message);
            throw new BusinessException(ErrorCodes.EMAIL_SENDING_FAILED, "Erreur lors de l'envoi de l'email");
        }
    }

    public async Task SendEmailAsync(
        string recipientEmail,
        string subject,
        string templateName,
        Dictionary<string, string> templateData)
    {
        try
        {
            var templatePath = Path.Combine(_templatesPath, $"{templateName}.html");
            if (!File.Exists(templatePath))
            {
                _logger.LogError("Template introuvable : {TemplatePath}", templatePath);
                throw new BusinessException(ErrorCodes.EMAIL_SENDING_FAILED, "Template non trouvé");
            }

            var htmlBody = await File.ReadAllTextAsync(templatePath);
            foreach (var data in templateData)
            {
                htmlBody = htmlBody.Replace($"[{data.Key}]", data.Value ?? "");
                subject = subject.Replace($"[{data.Key}]", data.Value ?? "");
            }

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailSettings.SenderName ?? "DOORZ",
                _emailSettings.SenderEmail ?? "noreply@doorz.be"));
            emailMessage.To.Add(new MailboxAddress("", recipientEmail));
            emailMessage.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = htmlBody };
            emailMessage.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort,
                _emailSettings.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.None);
            await client.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);

            _logger.LogInformation("Email envoyé à {RecipientEmail} avec le sujet '{Subject}'", recipientEmail, subject);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de l'envoi de l'email à {RecipientEmail}: {Message}", recipientEmail, ex.Message);
            throw new BusinessException(ErrorCodes.EMAIL_SENDING_FAILED, "Erreur lors de l'envoi de l'email");
        }
    }
}