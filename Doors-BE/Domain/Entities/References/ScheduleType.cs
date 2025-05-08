namespace Domain.Entities.References;

public class ScheduleType
{
    public int ScheduleTypeId { get; set; }
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Navigation property
    public ICollection<Offer> Offers { get; set; } = new List<Offer>();
    public ICollection<Degree> Degrees { get; set; } = new List<Degree>();
}