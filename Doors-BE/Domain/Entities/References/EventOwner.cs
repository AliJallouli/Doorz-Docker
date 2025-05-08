namespace Domain.Entities.References;

public class EventOwner : IEventOwner
{
    public int EventOwnerId { get; set; }
    public string OwnerType { get; set; } = null!;
    public DateTime CreatedAt { get; set; }

    // Navigation property
    public ICollection<Event> Events { get; set; } = new List<Event>();
}