namespace WebApi.Contracts.Responses;

public class ApiResponse<T>
{
    public SuccessResponse? Success { get; set; }
    public T? Data { get; set; }
    public ErrorResponse? Error { get; set; }

    // Réponse de succès simplifiée sans Message
    public static ApiResponse<T> Ok(T? data, string key)
    {
        return new ApiResponse<T> { Success = new SuccessResponse(key), Data = data };
    }

    // Réponse d'erreur simplifiée sans Message
    public static ApiResponse<T> Fail(string key, string? field,object? extraData = null)
    {
        return new ApiResponse<T> { Error = new ErrorResponse(key, field, extraData) };
    }
}