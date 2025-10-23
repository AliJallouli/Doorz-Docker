namespace Application.UseCases.Invitation.SuperAdmin.DTOs;

public class PublicOrganizationDto
{
    public int PublicOrganizationId { get; set; }
    public string Name { get; set; } = null!;
    public RoleDto Role { get; set; } = null!;
}