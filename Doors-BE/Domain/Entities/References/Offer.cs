

namespace Domain.Entities.References;

public class Offer
{
    public int OfferId { get; set; }
    public int CompanyId { get; set; }
    public int OfferTypeId { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public int? LocationId { get; set; }
    public short? Duration { get; set; }
    public int? DurationUnitId { get; set; }
    public short? WorkHours { get; set; }
    public int? StudentJobHours { get; set; }
    public decimal? Salary { get; set; }
    public DateTime? StartDate { get; set; }
    public int? ContractTypeId { get; set; }
    public int? ScheduleTypeId { get; set; }
    public int? EctsCredits { get; set; }
    public int LanguageId { get; set; }
    public bool RemotePossible { get; set; }
    public bool Sponsored { get; set; }
    public bool CvRequired { get; set; }
    public bool CoverLetterRequired { get; set; }
    public bool ExperienceRequired { get; set; }
    public DateTime? Deadline { get; set; }
    public bool Active { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }
    public int? PaymentId { get; set; }

    // Navigation properties
    public Company Company { get; set; } = null!;
    public OfferType OfferType { get; set; } = null!;
    public Location? Location { get; set; }
    public DurationUnit? DurationUnit { get; set; }
    public ContractType? ContractType { get; set; }
    public ScheduleType? ScheduleType { get; set; }
    public SpokenLanguage Language { get; set; } = null!;
    public Payment? Payment { get; set; }
    public Users? Creator { get; set; }
    public Users? Updater { get; set; }
    public ICollection<Application> Applications { get; set; } = new List<Application>();
    public ICollection<OfferFavorite> OfferFavorites { get; set; } = new List<OfferFavorite>();
    public ICollection<WorkHours> WorkHourss { get; set; } = new List<WorkHours>();
    public ICollection<OfferDegree> OfferDegrees { get; set; } = new List<OfferDegree>();
}