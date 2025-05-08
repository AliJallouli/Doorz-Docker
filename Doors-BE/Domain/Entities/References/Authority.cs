namespace Domain.Entities.References;

public class Authority
{
    public int AuthorityId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Navigation property
    public ICollection<Institution> Institutions { get; set; } = new List<Institution>();
}