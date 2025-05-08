namespace Domain.Exceptions;

public class BusinessException : Exception, IHasExtraData
{
    public BusinessException(string key, string? field = null, int statusCode = 400, object? extraData = null)
        : base(key)
    {
        Key = key;
        Field = field;
        StatusCode = statusCode;
        ExtraData = extraData;
    }

    public string Key { get; }
    public string? Field { get; }
    public int StatusCode { get; }
    public object? ExtraData { get; }
}