namespace Domain.Entities.References;

public class Notification
{
    public int NotificationId { get; set; }
    public int UserId { get; set; }
    public int NotificationTypeId { get; set; }
    public string? EntityType { get; set; }
    public int? EntityId { get; set; }
    public string Message { get; set; } = null!;
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Navigation properties
    public Users Users { get; set; } = null!;
    public NotificationType NotificationType { get; set; } = null!;
}