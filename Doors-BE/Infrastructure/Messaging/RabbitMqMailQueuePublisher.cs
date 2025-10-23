using System.Text;
using System.Text.Json;
using Domain.Messaging.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Infrastructure.Messaging
{
    public class RabbitMqMailQueuePublisher<T> : IMailQueuePublisher<T>, IDisposable
    {
        private readonly RabbitMqSettings _settings;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly ILogger< RabbitMqMailQueuePublisher<T> > _logger;

        public RabbitMqMailQueuePublisher(IOptions<RabbitMqSettings> options,ILogger<RabbitMqMailQueuePublisher<T>> logger)
        {
            _logger=logger;
            _settings = options.Value;

            var factory = new ConnectionFactory
            {
                HostName = _settings.HostName,
                Port = _settings.Port,
                UserName = _settings.UserName,
                Password = _settings.Password,
                VirtualHost = _settings.VirtualHost,
                DispatchConsumersAsync = true
            };

            if (_settings.UseSsl)
            {
                factory.Ssl = new SslOption
                {
                    Enabled = true,
                    ServerName = _settings.HostName,
                    Version = System.Security.Authentication.SslProtocols.Tls12,
                    AcceptablePolicyErrors =
                        System.Net.Security.SslPolicyErrors.RemoteCertificateChainErrors |
                        System.Net.Security.SslPolicyErrors.RemoteCertificateNameMismatch
                };
            }

            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                _channel.QueueDeclare(
                    queue: _settings.QueueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ RabbitMQ connection error: {ex.Message}");
                throw;
            }
        }

        public Task PublishAsync(T message, CancellationToken cancellationToken = default)
        {
            try
            {
                _channel.ExchangeDeclare(
                    exchange: "email_exchange",
                    type: ExchangeType.Direct,
                    durable: true
                );
                string json;
                try
                {
                     json= JsonSerializer.Serialize(message);
                    _logger.LogWarning("📤 Sérialisation OK : {Json}", json);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "❌ Échec de la sérialisation dans EmailAuthService !");
                    throw;
                }

                var body = Encoding.UTF8.GetBytes(json);

                var properties = _channel.CreateBasicProperties();
                properties.Persistent = true; // Message survivra aux redémarrages

                _channel.BasicPublish(
                    exchange: "email_exchange",
                    routingKey: _settings.QueueName,
                    basicProperties: properties, // <-- ICI
                    body: body
                );
                _logger.LogInformation("✅ Message publié dans {QueueName}", _settings.QueueName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Échec de la publication");
                throw;
            }
            return Task.CompletedTask;
        }        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
        }
    }
}
