namespace Domain.Entities.References;

public class ActivityType
{
    public int ActivityTypeId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }

    // Navigation properties
    public Users? Creator { get; set; }
    public Users? Updater { get; set; }
    public ICollection<UA> UAs { get; set; } = new List<UA>();
}