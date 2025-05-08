namespace Domain.Entities.References;

public class InstitutionCampusInvitation
{
    public int InstitutionCampusInvitationId { get; set; }
    public int CampusId { get; set; }
    public int InstitutionId { get; set; }
    public string Email { get; set; } = null!;
    public string InviteCode { get; set; } = null!;
    public int CampusRoleId { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool Used { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public Campus Campus { get; set; } = null!;
    public Institution Institution { get; set; } = null!;
}