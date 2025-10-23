namespace Application.UseCases.Auth.DTOs;

public class RefreshTokenResponseDto: AuthResponseDto
{
    public bool RememberMe { get; set; }
}