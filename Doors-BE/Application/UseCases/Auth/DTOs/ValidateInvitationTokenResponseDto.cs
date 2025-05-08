namespace Application.UseCases.Auth.DTOs;

public class ValidateInvitationTokenResponseDto
{
    public string InvitationToken { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int RoleId { get; set; }

    public int EntityId { get; set; } 

    public int EntityTypeId { get; set; }
    public string InvitationType { get; set; } = null!;
}