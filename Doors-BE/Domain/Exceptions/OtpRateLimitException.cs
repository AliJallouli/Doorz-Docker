namespace Domain.Exceptions;

public class OtpRateLimitException : BusinessException, IHasExtraData
{
    public OtpRateLimitException(string key, string? field = null, Dictionary<string, object>? extraData = null)
        : base(key, field)
    {
        ExtraData = extraData ?? new Dictionary<string, object>();
    }

    public new Dictionary<string, object> ExtraData { get; }
}
