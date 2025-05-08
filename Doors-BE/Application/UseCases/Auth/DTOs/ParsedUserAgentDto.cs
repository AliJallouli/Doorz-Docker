namespace Application.UseCases.Auth.DTOs;

public class ParsedUserAgentDto
{
    public string? Browser { get; set; }
    public string? OperatingSystem { get; set; }
    public string? DeviceType { get; set; }
}