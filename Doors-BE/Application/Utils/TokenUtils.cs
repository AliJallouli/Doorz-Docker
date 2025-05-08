namespace Application.Utils;

public static class TokenUtils
{
    public static string TruncateToken(string token, int visibleLength = 8)
    {
        if (string.IsNullOrWhiteSpace(token))
            return "[token vide]";

        return token.Length <= visibleLength
            ? token
            : token.Substring(0, visibleLength) + "...";
    }
}