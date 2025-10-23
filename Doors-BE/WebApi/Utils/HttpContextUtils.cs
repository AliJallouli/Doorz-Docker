namespace WebApi.Utils;

public static class HttpContextUtils
{
    private static readonly HashSet<string> SupportedLanguages = new() { "fr", "en", "nl", "de" };

    public static string ExtractLanguageCode(HttpRequest request)
    {
        var acceptLanguage = request.Headers["Accept-Language"].ToString();
        var languageCode = acceptLanguage.Split(',').FirstOrDefault()?.Split(';').FirstOrDefault() ?? "en";
        return SupportedLanguages.Contains(languageCode) ? languageCode : "en";
    }
    public static string ExtractClientIpAddress(HttpRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        return request.Headers["X-Forwarded-For"].FirstOrDefault()
               ?? request.HttpContext.Connection.RemoteIpAddress?.ToString()
               ?? "0.0.0.0";
    }

    public static string ExtractUserAgent(HttpRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        return request.Headers["User-Agent"].FirstOrDefault() ?? "Unknown";
    }
    
}