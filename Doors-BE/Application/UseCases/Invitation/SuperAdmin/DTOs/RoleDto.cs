namespace Application.UseCases.Invitation.SuperAdmin.DTOs;

public class RoleDto
{
    public int RoleId { get; set; }
    public string Name { get; set; } = null!;
    public int EntityTypeId { get; set; }
}