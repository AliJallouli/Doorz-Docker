namespace Domain.Entities.References;

public class EventInterest
{
    public int EventInterestId { get; set; }
    public int EventId { get; set; }
    public int StudentId { get; set; }
    public DateTime InterestDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }

    // Navigation properties
    public Event Event { get; set; } = null!;
    public Student Student { get; set; } = null!;
    public Users? Creator { get; set; }
    public Users? Updater { get; set; }
}