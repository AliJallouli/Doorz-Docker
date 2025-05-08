namespace Domain.Messaging.Interfaces;

public interface IMailQueuePublisher<T>
{
    Task PublishAsync(T message, CancellationToken cancellationToken = default);
}