namespace Application.UseCases.Auth.DTOs.Password;

public class ValidatePasswordResetTokenAndOtpRequestDto
{
    public string Token { get; set; } = null!;
    public string Otp { get; set; } = null!;
}