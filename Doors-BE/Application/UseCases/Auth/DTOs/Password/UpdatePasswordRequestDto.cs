namespace Application.UseCases.Auth.DTOs.Password;

public class UpdatePasswordRequestDto
{
    
    public string CurrentPassword { get; set; } = null!;

    
    public string NewPassword { get; set; } = null!;
}