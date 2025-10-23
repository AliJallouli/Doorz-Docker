namespace Application.UseCases.Auth.DTOs.EmailUser;

public class ConfirmEmailRequestDto
{
    public string? CodeOtp { get; set; }
    public string? Token { get; set; }
}