namespace Domain.Entities;

public class OtpSendLog
{
    public int OtpSendLogId { get; set; }

    public int SecurityTokenId { get; set; }
    public SecurityToken SecurityToken { get; set; } = null!;

    public DateTime SentAt { get; set; } = DateTime.UtcNow;

    public int? UserAgentId { get; set; }
    public UserAgent? UserAgent { get; set; }

    public string? IpAddress { get; set; }  
}