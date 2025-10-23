namespace Application.UseCases.Auth.DTOs.EmailUser;

public class UpdateEmailRequestDto
{
    public string CurrentPassword { get; set; } = string.Empty;
    public string NewEmail { get; set; } = string.Empty; 
}