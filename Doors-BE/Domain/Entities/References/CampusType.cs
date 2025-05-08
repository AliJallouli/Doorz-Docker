namespace Domain.Entities.References;

public class CampusType
{
    public int CampusTypeId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    // Navigation property
    public ICollection<Campus> Campuses { get; set; } = new List<Campus>();
}