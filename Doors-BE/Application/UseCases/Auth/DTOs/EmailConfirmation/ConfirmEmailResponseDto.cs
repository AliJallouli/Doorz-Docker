namespace Application.UseCases.Auth.DTOs.EmailConfirmation;

public class ConfirmEmailResponseDto
{
    public string Key { get; set; } = string.Empty;
    public OtpRegenerationMetadata? Metadata { get; set; }

}