namespace Application.UseCases.Invitation.SuperAdmin.DTOs;

public class CreatePublicOrganizationDto
{
    public string Name { get; set; } = null!;
    public string InvitationEmail { get; set; } = string.Empty;
}