namespace Domain.Entities.References;

public class CompanyInvitation
{
    public int CompanyInvitationId { get; set; }
    public int CompanyId { get; set; }
    public string Email { get; set; } = null!;
    public string InviteCode { get; set; } = null!;
    public int CompanyRoleId { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool Used { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public Company Company { get; set; } = null!;
}