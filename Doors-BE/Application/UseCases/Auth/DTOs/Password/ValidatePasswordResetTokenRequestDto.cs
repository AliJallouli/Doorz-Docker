namespace Application.UseCases.Auth.DTOs.Password;

public class ValidatePasswordResetTokenRequestDto
{
    public string Token { get; set; } = null!;
}