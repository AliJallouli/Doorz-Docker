namespace Application.UseCases.Invitation.Colleague.DTOs;

public class InviteColleagueRequestDto
{
    public string Email { get; set; } = null!;
    public int RoleId { get; set; }
}