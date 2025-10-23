namespace Application.Configurations;

public class 
    RateLimitSettings
{
    public int NameUpdateMaxAttempts { get; set; }
    public int NameUpdateTimeWindowSeconds { get; set; }
    public int EmailUpdateMaxAttempts { get; set; }
    public int EmailUpdateTimeWindowSeconds { get; set; }
    public int PasswordUpdateMaxAttempts { get; set; }
    public int PasswordUpdateTimeWindowSeconds { get; set; }
}