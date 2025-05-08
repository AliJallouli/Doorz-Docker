using Microsoft.Extensions.Logging;

namespace Application.Logger;

public interface IConsoleFormatter
{
    string Format(LogLevel logLevel, string category, EventId eventId, string message, Exception exception);
}