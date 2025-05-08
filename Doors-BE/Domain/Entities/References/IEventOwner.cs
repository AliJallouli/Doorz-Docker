namespace Domain.Entities.References;

public interface IEventOwner
{
    int EventOwnerId { get; set; }
    string OwnerType { get; set; }
    DateTime CreatedAt { get; set; }
    ICollection<Event> Events { get; set; }
}