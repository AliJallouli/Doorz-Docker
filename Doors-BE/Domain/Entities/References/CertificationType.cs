namespace Domain.Entities.References;

public class CertificationType
{
    public int CertificationTypeId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    // Navigation property
    public ICollection<Degree> Degrees { get; set; } = new List<Degree>();
}