namespace Application.UseCases.Auth.DTOs;

public class OtpRegenerationMetadata
{
    public int RemainingResends { get; set; }
    public int OtpAttemptsLeft { get; set; }
    public int ResendCount { get; set; }
}