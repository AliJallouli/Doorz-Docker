namespace Domain.Entities;

public class SessionEvent
{
    public int SessionEventId { get; set; }
    public int UserId { get; set; }
    public string EventType { get; set; } = null!; // "Login" ou "Logout"
    public string IpAddress { get; set; } = null!;
    public int? UserAgentId { get; set; }
    public UserAgent? UserAgent { get; set; }
    public DateTime EventTime { get; set; }

    // Navigation property (optionnelle)
    public Users User { get; set; } = null!;

    // Optionnel : si vous souhaitez accéder aux refresh tokens liés à cet événement
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}