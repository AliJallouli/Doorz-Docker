namespace Domain.Entities.References;

public class HousingVisit
{
    public int HousingVisitId { get; set; }
    public int StudentId { get; set; }
    public int HousingId { get; set; }
    public int? ApplicationStatusId { get; set; }
    public DateTime? ConfirmedDateTime { get; set; }
    public string? StudentMessage { get; set; }
    public string? LandlordMessage { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }

    // Navigation properties
    public Student Student { get; set; } = null!;
    public Housing Housing { get; set; } = null!;
    public ApplicationStatus? ApplicationStatus { get; set; }
    public Users? Creator { get; set; }
    public Users? Updater { get; set; }
    public ICollection<HousingVisitRange> HousingVisitRanges { get; set; } = new List<HousingVisitRange>();
}