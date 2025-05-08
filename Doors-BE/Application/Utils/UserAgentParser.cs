using System.Globalization;
using System.Text.RegularExpressions;
using Application.UseCases.Auth.DTOs;

namespace Application.Utils;

public static class UserAgentParser
{
    private const string Unknown = "Inconnu";

    private static readonly Regex BrowserRegex =
        new(
            @"(Chrome|Firefox|Safari|Edg(?:e)?|Opera|MSIE|Trident|SamsungBrowser|UCBrowser|Vivaldi|Brave)[\/\s]?([\d.]*)",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

    private static readonly Regex OsRegex =
        new(@"(Windows NT [\d.]+|Mac OS X [\d_]+|Linux(?: U)?|Android [\d.]+|iPhone|iPad)[^;)]*",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

    private static readonly Regex BotRegex = new(@"(Googlebot|Bingbot|YandexBot)",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    private static readonly Dictionary<string, string> OsMappings = new(StringComparer.OrdinalIgnoreCase)
    {
        { "Windows NT 11.0", "Windows 11" },
        { "Windows NT 10.0", "Windows 10" },
        { "Windows NT 6.3", "Windows 8.1" },
        { "Windows NT 6.2", "Windows 8" },
        { "Windows NT 6.1", "Windows 7" },
        { "Windows NT 5.1", "Windows XP" },
        { "Mac OS X", "macOS" }
    };

    private static readonly Dictionary<string, string> BrowserMappings = new(StringComparer.OrdinalIgnoreCase)
    {
        { "Edg", "Edge" },
        { "Edge", "Edge" },
        { "MSIE", "Internet Explorer" },
        { "Trident", "Internet Explorer" }
    };

    public static ParsedUserAgentDto Parse(string userAgent)
    {
        if (string.IsNullOrWhiteSpace(userAgent))
            return DefaultResult();

        if (BotRegex.IsMatch(userAgent))
            return new ParsedUserAgentDto { Browser = "Bot", OperatingSystem = "N/A", DeviceType = "Bot" };

        var browser = ParseBrowser(userAgent);
        var os = ParseOperatingSystem(userAgent);
        var deviceType = DetectDeviceType(userAgent);

        return new ParsedUserAgentDto
        {
            Browser = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(browser),
            OperatingSystem = os,
            DeviceType = deviceType,
        };
    }

    private static ParsedUserAgentDto DefaultResult()
    {
        return new ParsedUserAgentDto { Browser = Unknown, OperatingSystem = Unknown, DeviceType = Unknown };
    }

    private static string ParseBrowser(string userAgent)
    {
        var match = BrowserRegex.Match(userAgent);
        if (!match.Success) return "Navigateur inconnu";

        var name = match.Groups[1].Value;
        var version = match.Groups[2].Value;
        if (BrowserMappings.TryGetValue(name, out var mappedName))
            name = mappedName;

        return string.IsNullOrEmpty(version) ? name : $"{name} {version}";
    }

    private static string ParseOperatingSystem(string userAgent)
    {
        var match = OsRegex.Match(userAgent);
        if (!match.Success) return Unknown;

        var osRaw = match.Value.Replace("_", ".").Trim();
        foreach (var (key, value) in OsMappings)
            if (osRaw.StartsWith(key, StringComparison.OrdinalIgnoreCase))
                return osRaw.Length > key.Length ? $"{value} {osRaw[key.Length..].Trim()}" : value;

        return osRaw;
    }

    private static string DetectDeviceType(string userAgent)
    {
        var ua = userAgent.ToLowerInvariant();
        return ua switch
        {
            _ when ua.Contains("smarttv") || ua.Contains("tizen") || ua.Contains("webos") => "Smart TV",
            _ when ua.Contains("playstation") || ua.Contains("xbox") => "Console",
            _ when ua.Contains("mobile") && !ua.Contains("tablet") => "Mobile",
            _ when ua.Contains("tablet") || ua.Contains("ipad") => "Tablet",
            _ when (ua.Contains("windows") || ua.Contains("macintosh") || ua.Contains("linux")) &&
                   !ua.Contains("mobile") => "Desktop",
            _ => Unknown
        };
    }
}