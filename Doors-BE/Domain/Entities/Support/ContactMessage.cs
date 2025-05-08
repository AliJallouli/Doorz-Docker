namespace Domain.Entities.Support;
using Domain.Entities.References;

public class ContactMessage
{
    public int ContactMessageId { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string Message { get; set; } = null!;
    public int LanguageId { get; set; }
    public SpokenLanguage Language { get; set; } = null!;
    public int? UserId { get; set; }
    public Users? Users { get; set; }
    public string IpAddress { get; set; } = null!;
    public int? UserAgentId { get; set; }
    public UserAgent? UserAgent { get; set; }
    public DateTime ReceivedAt { get; set; } = DateTime.UtcNow;
    public int? ContactMessageTypeId { get; set; }
    public string? Phone { get; set; }

    public ContactMessageType? ContactMessageType { get; set; }
}