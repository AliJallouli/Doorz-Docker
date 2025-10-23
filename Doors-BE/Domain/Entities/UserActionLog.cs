namespace Domain.Entities;

public class UserActionLog
{
    public int UserActionLogId { get; set; }
    public int UserId { get; set; }
    public string ActionType { get; set; } = string.Empty;
    public DateTime ActionTimestamp { get; set; }
    public int UserAgentId { get; set; }
    public string IpAddress { get; set; } = string.Empty;
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }

    // Propriétés de navigation (optionnel)
    public Users User { get; set; } = null!;
    public UserAgent UserAgent { get; set; } = null!;
}