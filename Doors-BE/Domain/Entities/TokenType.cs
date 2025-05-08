namespace Domain.Entities;

public class TokenType:IHasUpdatedAt,IHasCreatedAt
{
    public int TokenTypeId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int DefaultTokenExpirationMinutes { get; set; }
    public int DefaultOtpExpirationMinutes{ get; set; }
    
    public int MinDelayMinutes { get; set; }
    public int MaxRequestsPerWindow { get; set; }
    public int RateLimitWindowMinutes { get; set; } 
    public bool IsRateLimited { get; set; }
    
    public bool TokenRequired { get; set; }           
    public bool CodeOtpRequired { get; set; }   
    public int MaxOtpAttempts { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Navigation
    public ICollection<SecurityToken> SecurityTokens { get; set; } = new List<SecurityToken>();
}