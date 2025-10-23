namespace Application.UseCases.Invitation.SuperAdmin.DTOs;

public class CreateAssociationDto
{
    public string Name { get; set; } = null!;
    public string InvitationEmail { get; set; } = string.Empty;
}