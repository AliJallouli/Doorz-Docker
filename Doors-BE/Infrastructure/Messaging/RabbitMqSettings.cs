namespace Infrastructure.Messaging;

/// <summary>
/// Paramètres de configuration RabbitMQ, liés à la section "RabbitMQ" du fichier appsettings.
/// </summary>
public class RabbitMqSettings
{
    /// <summary>
    /// Hôte RabbitMQ (localhost, IP ou DNS).
    /// </summary>
    public string HostName { get; set; } = string.Empty;

    /// <summary>
    /// Port d'écoute RabbitMQ (5672 par défaut, ou 5671 si TLS).
    /// </summary>
    public int Port { get; set; } = 5672;

    /// <summary>
    /// Nom d'utilisateur RabbitMQ (éviter "guest" en production).
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Mot de passe RabbitMQ.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Nom de la file utilisée pour les e-mails.
    /// </summary>
    public string QueueName { get; set; } = "email_queue";

    /// <summary>
    /// Virtual host RabbitMQ (par défaut : "/").
    /// </summary>
    public string VirtualHost { get; set; } = "/";
    public bool UseSsl { get; set; } = false;
}