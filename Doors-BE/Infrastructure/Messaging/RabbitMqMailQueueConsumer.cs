using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Messaging;

public class RabbitMqMailQueueConsumer : BackgroundService
{
    private readonly ILogger<RabbitMqMailQueueConsumer> _logger;
    private readonly RabbitMqSettings _settings;
    private IConnection? _connection;
    private IModel? _channel;
    private readonly IServiceProvider _serviceProvider;

    public RabbitMqMailQueueConsumer(IOptions<RabbitMqSettings> options, ILogger<RabbitMqMailQueueConsumer> logger, IServiceProvider serviceProvider)
    {
        _settings = options.Value;
        _logger = logger;
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = _settings.HostName,
            Port = _settings.Port,
            UserName = _settings.UserName,
            Password = _settings.Password,
            VirtualHost = _settings.VirtualHost,
            DispatchConsumersAsync = true,

        };

        if (_settings.UseSsl)
        {
            factory.Ssl = new SslOption
            {
                Enabled = true,
                ServerName = _settings.HostName,
                Version = System.Security.Authentication.SslProtocols.Tls13,
                AcceptablePolicyErrors =
                    System.Net.Security.SslPolicyErrors.RemoteCertificateChainErrors |
                    System.Net.Security.SslPolicyErrors.RemoteCertificateNameMismatch
            };
        }

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(
            queue: _settings.QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );
        _channel.ExchangeDeclare(
            exchange: "email_exchange",
            type: ExchangeType.Direct,
            durable: true
        );
        _channel.QueueBind(
            queue: _settings.QueueName,
            exchange: "email_exchange",
            routingKey: _settings.QueueName
        );

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var messageJson = Encoding.UTF8.GetString(body);

            _logger.LogInformation("📥 Message reçu de la file {QueueName}: {MessageJson}", _settings.QueueName,
                messageJson);

            if (string.IsNullOrWhiteSpace(messageJson))
            {
                _logger.LogWarning("Message vide reçu de la file {QueueName}, ignoré", _settings.QueueName);
                _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                await Task.Yield();
                return;
            }

            try
            {
                var message = JsonSerializer.Deserialize<SendTemplatedEmailMessage>(messageJson);
                _logger.LogInformation("Message désérialisé : {@Message}", message);
                using var scope = _serviceProvider.CreateScope();
                var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                if (message != null)
                {
                    await emailService.SendEmailAsync(
                        message.To,
                        message.Subject,
                        message.TemplateName,
                        message.TemplateData,
                        message.Language ?? "fr",
                        message.From ?? "noreply@doorz.be",
                        message.FromName ?? "DOORZ"
                    );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors du traitement du message : {MessageJson}", messageJson);
            }

            _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            await Task.Yield();
        };

        _channel.BasicConsume(
            queue: _settings.QueueName,
            autoAck: false,
            consumer: consumer
        );

        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
        base.Dispose();
    }
}



