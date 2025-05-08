namespace Domain.Entities.References;

public class DegreeCategory
{
    public int DegreeCategoryId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    // Navigation property
    public ICollection<Degree> Degrees { get; set; } = new List<Degree>();
}