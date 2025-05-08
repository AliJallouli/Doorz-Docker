namespace Domain.Entities.References;
public class InstitutionInvitation
{
    public int InstitutionInvitationId { get; set; }
    public int InstitutionId { get; set; }
    public string Email { get; set; } = null!;
    public string InviteCode { get; set; } = null!;
    public int InstitutionRoleId { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool Used { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public Institution Institution { get; set; } = null!;
}