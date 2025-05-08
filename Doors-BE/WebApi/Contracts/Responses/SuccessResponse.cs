namespace WebApi.Contracts.Responses;

public class SuccessResponse
{
    public SuccessResponse(string key)
    {
        Key = key;
    }

    public string Key { get; set; }
}