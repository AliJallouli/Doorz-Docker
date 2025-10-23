namespace Application.UseCases.Invitation.SuperAdmin.DTOs;

public class CreateStudentMovementDto
{
    public string Name { get; set; } = null!;
    public string InvitationEmail { get; set; } = string.Empty;
}