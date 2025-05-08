namespace Domain.Entities.References;

public class DegreeType
{
    public int DegreeTypeId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int CycleId { get; set; }

    // Navigation properties
    public Cycle Cycle { get; set; } = null!;
    public ICollection<Degree> Degrees { get; set; } = new List<Degree>();
}