namespace Application.UseCases.UsersSite.DTOs;

public class UserResponseDTO
{
    public int UserId { get; set; }
    public string Email { get; set; } = default!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string SuperRole { get; set; } = default!;
    public string? Role { get; set; }
    public string? EntityName { get; set; }
    public string? EntityType { get; set; }
    public bool IsVerified { get; set; }
}