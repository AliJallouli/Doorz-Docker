namespace Domain.Entities.References;

public class FacilityType
{
    public int FacilityTypeId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    // Navigation property
    public ICollection<CampusFacility> CampusFacilities { get; set; } = new List<CampusFacility>();
}