namespace Application.UseCases.Auth.DTOs;

public class ValidateInvitationTokenRequestDto
{
    public string InvitationToken { get; set; }= string.Empty;
}