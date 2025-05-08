namespace Application.UseCases.Auth.DTOs.Password;

public class ConfirmPasswordResetRequestDto
{
    public string Token { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
}