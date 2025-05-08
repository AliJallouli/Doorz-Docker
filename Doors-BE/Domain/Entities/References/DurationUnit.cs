namespace Domain.Entities.References;

public class DurationUnit
{
    public int DurationUnitId { get; set; }
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }

    // Navigation properties
    public Users? Creator { get; set; }
    public Users? Updater { get; set; }
    public ICollection<Offer> Offers { get; set; } = new List<Offer>();
    public ICollection<Degree> Degrees { get; set; } = new List<Degree>();
}