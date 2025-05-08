namespace Domain.Entities.References;

public class Semester
{
    public int SemesterId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    // Navigation property
    public ICollection<UE> UEs { get; set; } = new List<UE>();
}