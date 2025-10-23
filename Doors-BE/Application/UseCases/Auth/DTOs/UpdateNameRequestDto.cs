namespace Application.UseCases.Auth.DTOs;

public class UpdateNameRequestDto
{
    public string CurrentPassword { get; set; } = string.Empty; 
    public string? NewFirstName { get; set; } 
    public string? NewLastName { get; set; } 
}