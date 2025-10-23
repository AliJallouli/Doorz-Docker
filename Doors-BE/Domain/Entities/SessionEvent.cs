namespace Domain.Entities;

public class SessionEvent
{
    public int SessionEventId { get; set; }

    // Liens
    public int UserId { get; set; }
    public Users User { get; set; } = null!;

    public int? UserAgentId { get; set; }
    public UserAgent? UserAgent { get; set; }

    // Infos de session
    public string IpAddress { get; set; } = null!;
    public bool RememberMe { get; set; }
    public bool IsRevoked { get; set; }

    public DateTime OpenedAt { get; set; }
    public string? OpeningReason { get; set; }

    public DateTime? ClosedAt { get; set; }
    public string? ClosingReason { get; set; }

    public DateTime? LastSeenAt { get; set; }

    // Tokens liés à cette session
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}