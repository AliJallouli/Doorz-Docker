namespace Application.UseCases.Invitation.SuperAdmin.DTOs;

public class CompanyDto
{
    public int CompanyId { get; set; }
    public string Name { get; set; } = null!;
    public string CompanyNumber { get; set; }=string.Empty;
    public RoleDto Role { get; set; } = null!;
}