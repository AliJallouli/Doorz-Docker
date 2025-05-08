using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;

namespace Application.Logger;

/// <summary>
///     Formateur personnalisé pour les logs de la console, ajoutant des préfixes "infosystem" ou "infoplatform" selon la
///     catégorie.
/// </summary>
public class CustomLoggerFormatter : ConsoleFormatter
{
    /// <summary>
    ///     Initialise une nouvelle instance de <see cref="CustomLoggerFormatter" />.
    /// </summary>
    public CustomLoggerFormatter() : base("CustomFormatter")
    {
    }

    /// <summary>
    ///     Formate un message de log pour la console.
    /// </summary>
    /// <param name="logEntry">L'entrée de log contenant les détails.</param>
    /// <param name="scopeProvider">Fournisseur de portée pour les scopes de logging.</param>
    /// <param name="textWriter">Le writer utilisé pour écrire le message.</param>
    public override void Write<TState>(
        in LogEntry<TState> logEntry,
        IExternalScopeProvider? scopeProvider,
        TextWriter textWriter)
    {
        var message = logEntry.Formatter(logEntry.State, logEntry.Exception);
        if (string.IsNullOrEmpty(message))
            return;

        var category = logEntry.Category ?? "UnknownCategory";
        var prefix = category.StartsWith("Microsoft.EntityFrameworkCore") ? "infosystem" : "infoplatform";
        var formattedMessage = $"{prefix}: [{logEntry.LogLevel}] {category}[{logEntry.EventId.Id}] {message}";

        if (logEntry.Exception != null)
            formattedMessage += $"\nException: {logEntry.Exception.Message} - {logEntry.Exception.StackTrace}";

        textWriter.WriteLine(formattedMessage);
    }
}