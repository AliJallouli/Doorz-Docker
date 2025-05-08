namespace Domain.Entities.References;

public class CampusFacility
{
    public int CampusFacilityId { get; set; }
    public int CampusId { get; set; }
    public int FacilityTypeId { get; set; }
    public int Quantity { get; set; }
    public string? Details { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }

    // Navigation properties
    public Campus Campus { get; set; } = null!;
    public FacilityType FacilityType { get; set; } = null!;
    public Users? Creator { get; set; }
    public Users? Updater { get; set; }
}