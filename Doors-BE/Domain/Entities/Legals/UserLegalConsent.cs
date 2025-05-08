namespace Domain.Entities.Legals;

public class UserLegalConsent
{
    public int ConsentId { get; set; }

    public int UserId { get; set; }
    public Users User { get; set; } = null!;

    public int DocumentId { get; set; }
    public LegalDocument Document { get; set; } = null!;

    public string DocumentVersion { get; set; } = null!;
    public DateTime AcceptedAt { get; set; }
    public string? IpAddress { get; set; }

    public int? UserAgentId { get; set; }
    public UserAgent? UserAgent { get; set; }

    public bool Revoked { get; set; } = false;
    public DateTime? RevokedAt { get; set; }
    public string? RevokeReason { get; set; }
}
