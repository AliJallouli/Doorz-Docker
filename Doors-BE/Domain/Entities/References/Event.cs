namespace Domain.Entities.References;

public class Event
{
    public int EventId { get; set; }
    public int EventOwnerId { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public int LocationId { get; set; }
    public bool RegistrationRequired { get; set; }
    public string? RegistrationLink { get; set; }
    public bool IsPublic { get; set; }
    public int LikeCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }
    public int? PaymentId { get; set; }

    // Navigation properties
    public EventOwner EventOwner { get; set; } = null!;

    public Location Location { get; set; } = null!;
    public Payment? Payment { get; set; }
    public Users? Creator { get; set; }
    public Users? Updater { get; set; }
    public ICollection<EventInterest> EventInterests { get; set; } = new List<EventInterest>();
}