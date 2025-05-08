namespace Domain.Entities.References;

public class Cycle
{
    public int CycleId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    // Navigation property
    public ICollection<DegreeType> DegreeTypes { get; set; } = new List<DegreeType>();
}