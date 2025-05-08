namespace Domain.Entities.References;

public class Housing
{
    public int HousingId { get; set; }
    public int HousingOwnerId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public int LocationId { get; set; }
    public decimal Charges { get; set; }
    public decimal Deposit { get; set; }
    public decimal? Size { get; set; }
    public int? BedroomCount { get; set; }
    public int? Capacity { get; set; }
    public int HousingTypeId { get; set; }
    public int PebRatingId { get; set; }
    public string Status { get; set; } = "Disponible";
    public DateTime? AvailabilityDate { get; set; }
    public DateTime? EndAvailabilityDate { get; set; }
    public string? Preferences { get; set; }
    public bool LegalCompliance { get; set; }
    public bool Sponsored { get; set; }
    public int ViewCount { get; set; }
    public int ApplicationCount { get; set; }
    public int LikeCount { get; set; }
    public int VisitCount { get; set; }
    public DateTime? LastViewedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? PaymentId { get; set; }

    // Navigation properties
    public HousingOwner HousingOwner { get; set; } = null!; // Change from IHousingOwner to HousingOwner
    public Location Location { get; set; } = null!;
    public HousingType HousingType { get; set; } = null!;
    public PebRating PebRating { get; set; } = null!;
    public Payment? Payment { get; set; }
    public ICollection<HousingAmenity> HousingAmenities { get; set; } = new List<HousingAmenity>();
    public ICollection<HousingApplication> HousingApplications { get; set; } = new List<HousingApplication>();
    public ICollection<HousingVisit> HousingVisits { get; set; } = new List<HousingVisit>();
}