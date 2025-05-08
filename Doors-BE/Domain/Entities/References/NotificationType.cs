namespace Domain.Entities.References;

public class NotificationType
{
    public int NotificationTypeId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string Priority { get; set; } = "Medium";
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Navigation property
    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}