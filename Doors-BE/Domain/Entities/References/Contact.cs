namespace Domain.Entities.References;

public class Contact
{
    public int ContactId { get; set; }
    public int? LocationId { get; set; }
    public string? Phone { get; set; }
    public string? ContactEmail { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }

    // Navigation properties
    public Location? Location { get; set; }
    public Users? Creator { get; set; }
    public Users? Updater { get; set; }
    public ICollection<Student> Students { get; set; } = new List<Student>();
    public ICollection<Institution> Institutions { get; set; } = new List<Institution>();
    public ICollection<Campus> Campuses { get; set; } = new List<Campus>();
    public ICollection<Company> Companies { get; set; } = new List<Company>();
    public ICollection<Landlord> Landlords { get; set; } = new List<Landlord>();
    public ICollection<Association> Associations { get; set; } = new List<Association>();
    public ICollection<PublicOrganization> PublicOrganizations { get; set; } = new List<PublicOrganization>();
    public ICollection<StudentMovement> StudentMovements { get; set; } = new List<StudentMovement>();
}