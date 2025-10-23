namespace Application.Configurations;

public class AuthSettings
{
    public int FailedLoginAttemptsWindowMinutes { get; set; }
    public int MaxFailedLoginAttempts { get; set; }
    public int ShortLivedRefreshTokenDays { get; set; } 
    public int LongLivedRefreshTokenDays { get; set; }  
    public int RefreshTokenLockTimeoutSeconds { get; set; }
    public int RefreshTokenReuseThresholdMinutes { get; set; }
}