namespace Domain.Entities.References;

public class StudyDomain
{
    public int DomainId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    // Navigation property
    public ICollection<Specialty> Specialties { get; set; } = new List<Specialty>();
}