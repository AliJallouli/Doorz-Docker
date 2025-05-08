namespace Domain.Entities.References;

public class UA
{
    public int UaId { get; set; }
    public int? UeId { get; set; }
    public string Name { get; set; } = null!;
    public int ActivityTypeId { get; set; }
    public int CreditCount { get; set; }
    public string? Description { get; set; }
    public bool Mandatory { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }

    // Navigation properties
    public UE? UE { get; set; }
    public ActivityType ActivityType { get; set; } = null!;
    public Users? Creator { get; set; }
    public Users? Updater { get; set; }
}