namespace Application.UseCases.Auth.DTOs.EmailUser;

public class ConfirmEmailResponseDto
{
    public string Key { get; set; } = string.Empty;
    public OtpRegenerationMetadata? Metadata { get; set; }

}