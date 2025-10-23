namespace Application.UseCases.Invitation.SuperAdmin.DTOs;

public class AssociationDto
{
    public int AssociationId { get; set; }
    public string Name { get; set; } = null!;
    public RoleDto Role { get; set; } = null!;
}