namespace WebApi.Contracts.Responses;

public class ErrorResponse
{
    public ErrorResponse(string key, string? field, object? extraData = null)
    {
        Key = key;
        Field = field;
        ExtraData = extraData;
    }

    public string Key { get; set; }
    public string? Field { get; set; }
    public object? ExtraData { get; set; }
}