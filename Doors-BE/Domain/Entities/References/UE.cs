namespace Domain.Entities.References;

public class UE
{
    public int UeId { get; set; }
    public int? SpecialtyId { get; set; }
    public string Name { get; set; } = null!;
    public int Year { get; set; }
    public int SemesterId { get; set; }
    public int CreditCount { get; set; }
    public string? Description { get; set; }
    public bool Mandatory { get; set; }
    public int? PrerequisiteUeId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }

    // Navigation properties
    public Specialty? Specialty { get; set; }
    public Semester Semester { get; set; } = null!;
    public UE? PrerequisiteUE { get; set; }
    public Users? Creator { get; set; }
    public Users? Updater { get; set; }
    public ICollection<UA> UAs { get; set; } = new List<UA>();
    public ICollection<UE> DependentUEs { get; set; } = new List<UE>();
}