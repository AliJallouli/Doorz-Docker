namespace Application.UseCases.Support.DTOs;

public class CreateContactMessageRequestDto
{
    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Subject { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public int LanguageId { get; set; } 
    
    public int? ContactMessageTypeId { get; set; }
    public string? Phone { get; set; } 
    

    // Ces champs sont remplis automatiquement côté backend
    public string? IpAddress { get; set; }

    public int? UserAgentId { get; set; }

    public int? UserId { get; set; }
}