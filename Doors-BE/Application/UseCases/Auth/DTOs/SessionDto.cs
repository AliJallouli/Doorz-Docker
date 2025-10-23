namespace Application.UseCases.Auth.DTOs;

public class SessionDto
{
    public int SessionEventId { get; set; }
    public string IpAddress { get; set; } = null!;
    public int? UserAgentId { get; set; }
    public string? UserAgent { get; set; } 
    public DateTime OpenedAt { get; set; }
    public DateTime? LastSeenAt { get; set; }
    public bool RememberMe { get; set; }
}