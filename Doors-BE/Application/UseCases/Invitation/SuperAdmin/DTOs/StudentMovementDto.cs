namespace Application.UseCases.Invitation.SuperAdmin.DTOs;

public class StudentMovementDto
{
    public int StudentMovementId { get; set; }
    public string Name { get; set; } = null!;
    public RoleDto Role { get; set; } = null!;
}