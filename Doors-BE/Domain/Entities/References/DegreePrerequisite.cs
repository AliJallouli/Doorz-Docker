namespace Domain.Entities.References;

public class DegreePrerequisite
{
    public int DegreePrerequisiteId { get; set; }
    public int DegreeId { get; set; }
    public int PrerequisiteTypeId { get; set; }
    public int PrerequisiteSourceId { get; set; }
    public string Description { get; set; } = null!;
    public int? RequiredDegreeId { get; set; }
    public decimal? MinimumGrade { get; set; }
    public bool Mandatory { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }

    // Navigation properties
    public Degree Degree { get; set; } = null!;
    public PrerequisiteType PrerequisiteType { get; set; } = null!;
    public PrerequisiteSource PrerequisiteSource { get; set; } = null!;
    public Degree? RequiredDegree { get; set; }
    public Users? Creator { get; set; }
    public Users? Updater { get; set; }
}