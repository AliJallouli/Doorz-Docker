namespace Domain.Entities.References;

public class DegreePartnership
{
    public int DegreePartnershipId { get; set; }
    public int DegreeId { get; set; }
    public int? CompanyId { get; set; }
    public string? PartnerName { get; set; }
    public string? PartnershipType { get; set; }
    public string? Role { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Navigation properties
    public Degree Degree { get; set; } = null!;
    public Company? Company { get; set; }
}