namespace BackEnd_TI.Utils;

public static class LanguageUtils
{
    private static readonly HashSet<string> SupportedLanguages = new() { "fr", "en", "nl", "de" };

    public static string ExtractLanguageCode(HttpRequest request)
    {
        var acceptLanguage = request.Headers["Accept-Language"].ToString();
        var languageCode = acceptLanguage.Split(',').FirstOrDefault()?.Split(';').FirstOrDefault() ?? "en";
        return SupportedLanguages.Contains(languageCode) ? languageCode : "en";
    }
}