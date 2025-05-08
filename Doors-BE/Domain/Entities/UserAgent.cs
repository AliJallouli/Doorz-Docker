namespace Domain.Entities;

public class UserAgent:IHasUpdatedAt,IHasCreatedAt
{
    public int UserAgentId { get; set; }
    public string? UserAgentValue { get; set; }
    public string? Browser { get; set; }
    public string? OperatingSystem { get; set; }
    public string? DeviceType { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}