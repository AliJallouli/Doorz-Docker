namespace Domain.Entities.References;

public class PrerequisiteSource
{
    public int PrerequisiteSourceId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    // Navigation property
    public ICollection<DegreePrerequisite> DegreePrerequisites { get; set; } = new List<DegreePrerequisite>();
}