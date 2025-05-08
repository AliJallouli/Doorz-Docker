namespace Application.UseCases.Auth.DTOs;

public class AuthResponseDto
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public string Message { get; set; } = null!;
}