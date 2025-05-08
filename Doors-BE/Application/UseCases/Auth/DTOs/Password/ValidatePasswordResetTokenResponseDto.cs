namespace Application.UseCases.Auth.DTOs.Password;

public class ValidatePasswordResetTokenResponseDto
{
    public string Key { get; set; }=string.Empty;
    public string Email { get; set; }=string.Empty;
}