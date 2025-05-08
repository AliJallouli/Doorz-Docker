namespace Domain.Entities.References;

public class ApplicationStatus
{
    public int ApplicationStatusId { get; set; }
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }

    // Navigation properties
    public Users? Creator { get; set; }
    public Users? Updater { get; set; }
    public ICollection<HousingApplication> HousingApplications { get; set; } = new List<HousingApplication>();
    public ICollection<HousingVisit> HousingVisits { get; set; } = new List<HousingVisit>();
    public ICollection<Application> Applications { get; set; } = new List<Application>();
}