namespace Application.UseCases.Invitation.EntityAdmin.ColleagueInvitation.DTOs;

public class InviteColleagueRequestDto
{
    public string Email { get; set; } = null!;
    public int RoleId { get; set; }
}