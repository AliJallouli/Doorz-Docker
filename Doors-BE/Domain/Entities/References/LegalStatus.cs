namespace Domain.Entities.References;

public class LegalStatus
{
    public int LegalStatusId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    // Navigation property
    public ICollection<Institution> Institutions { get; set; } = new List<Institution>();
}