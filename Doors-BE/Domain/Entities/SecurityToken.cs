namespace Domain.Entities;

public class SecurityToken:IHasUpdatedAt,IHasCreatedAt
{
    public int SecurityTokenId { get; set; }
    public int UserId { get; set; }
    public int TokenTypeId { get; set; }
    public string TokenHash { get; set; } = null!;
    public string? CodeOtpHash { get; set; }
    public int OtpAttemptCount { get; set; }
    public string? IpAddress { get; set; }
    public int? UserAgentId { get; set; }
    public UserAgent? UserAgent { get; set; }
    public string? DeviceId { get; set; }
    public string? Metadata { get; set; }

    public DateTime TokenExpiresAt { get; set; }
    public DateTime? OtpExpiresAt { get; set; }
    public DateTime? OtpRevokedAt { get; set; }

    public bool Used { get; set; }
    public bool Revoked { get; set; } 
    public string? RevokeReason { get; set; }
    public DateTime? RevokedAt { get; set; }

    public DateTime? ConsumedAt { get; set; }

    public int ResendCount { get; set; } 
    public DateTime? LastSentAt { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Users User { get; set; } = null!;
    public TokenType TokenType { get; set; } = null!;
}