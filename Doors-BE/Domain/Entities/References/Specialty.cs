namespace Domain.Entities.References;

public class Specialty
{
    public int SpecialtyId { get; set; }
    public int? DegreeId { get; set; }
    public int DomainId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? Outcomes { get; set; }
    public bool OfficiallyRecognized { get; set; }
    public int LikeCount { get; set; }
    public int VisitCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }

    // Navigation properties
    public Degree? Degree { get; set; }
    public StudyDomain Domain { get; set; } = null!;
    public Users? Creator { get; set; }
    public Users? Updater { get; set; }
    public ICollection<UE> UEs { get; set; } = new List<UE>();
}