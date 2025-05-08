namespace Domain.Entities.References;
public class Degree
{
    public int DegreeId { get; set; }
    public int? CampusId { get; set; }
    public string Name { get; set; } = null!;
    public int DegreeTypeId { get; set; }
    public int? DegreeCategoryId { get; set; }
    public int? Duration { get; set; }
    public int? DurationUnitId { get; set; }
    public int? Credits { get; set; }
    public decimal? Cost { get; set; }
    public int TuitionTypeId { get; set; }
    public int? LanguageId { get; set; }
    public int ScheduleTypeId { get; set; }
    public bool IsAlternance { get; set; }
    public int? CertificationTypeId { get; set; }
    public int DeliveryModeId { get; set; }
    public int? QualificationLevel { get; set; }
    public bool IsActive { get; set; }
    public int LikeCount { get; set; }
    public int VisitCount { get; set; }
    public string? Description { get; set; }
    public int? FinancabilityRequiredCredits { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }

    // Navigation properties
    public Campus? Campus { get; set; }
    public DegreeType DegreeType { get; set; } = null!;
    public DegreeCategory? DegreeCategory { get; set; }
    public DurationUnit? DurationUnit { get; set; }
    public TuitionType TuitionType { get; set; } = null!;
    public SpokenLanguage? Language { get; set; }
    public ScheduleType ScheduleType { get; set; } = null!;
    public CertificationType? CertificationType { get; set; }
    public DeliveryMode DeliveryMode { get; set; } = null!;
    public Users? Creator { get; set; }
    public Users? Updater { get; set; }
    public ICollection<Specialty> Specialties { get; set; } = new List<Specialty>();
    public ICollection<DegreePartnership> DegreePartnerships { get; set; } = new List<DegreePartnership>();
    public ICollection<OfferDegree> OfferDegrees { get; set; } = new List<OfferDegree>();
    public ICollection<Bridge> BridgesAsFrom { get; set; } = new List<Bridge>();
    public ICollection<Bridge> BridgesAsTo { get; set; } = new List<Bridge>();
    public ICollection<DegreePrerequisite> DegreePrerequisites { get; set; } = new List<DegreePrerequisite>();
}