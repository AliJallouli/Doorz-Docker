namespace Domain.Entities;

public class LoginAttempt
{
    public int LoginAttemptId { get; set; }
    public int? UserId { get; set; }
    public string Email { get; set; } = null!;
    public string IpAddress { get; set; } = null!;
    public int? UserAgentId { get; set; }
    public UserAgent? UserAgent { get; set; }
    public DateTime AttemptTime { get; set; }
    public DateTime? LockedUntil { get; set; }
    public bool Success { get; set; }

    // Navigation property
    public Users? User { get; set; }
}