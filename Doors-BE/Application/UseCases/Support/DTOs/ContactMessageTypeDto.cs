namespace Application.UseCases.Support.DTOs;

public class ContactMessageTypeDto
{
    public int ContactMessageTypeId { get; set; }
    public string KeyName { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public int Priority { get; set; }
    public bool IsActive { get; set; }
    
}