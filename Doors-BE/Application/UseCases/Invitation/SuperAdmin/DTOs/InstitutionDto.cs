namespace Application.UseCases.Invitation.SuperAdmin.DTOs;

public class InstitutionDto
{
    public int InstitutionId { get; set; }
    public string Name { get; set; } = null!;
    public int InstitutionTypeId { get; set; }
    public RoleDto Role { get; set; } = null!;
}