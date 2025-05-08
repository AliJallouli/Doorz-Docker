namespace Domain.Entities;

public class SuperadminInvitation:IHasCreatedAt
{
    public int SuperadminInvitationId { get; set; }
    public string Email { get; set; } = null!; // Non nullable, UNIQUE
    public string InvitationToken { get; set; } = null!; // Non nullable, UNIQUE
    public DateTime ExpiresAt { get; set; }
    public bool Used { get; set; }
    public DateTime CreatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public int InvitationTypeId { get; set; } // ✅ nouvelle FK
    public InvitationType InvitationType { get; set; } = null!;

    // Relation de navigation (1:1 avec SuperadminInvitationEntity)
    public SuperadminInvitationEntity SuperadminInvitationEntity { get; set; } = null!;

    // Propriété de navigation
    public Users? CreatedByUser { get; set; }
}