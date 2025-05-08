namespace Domain.Entities.References;

public class CampusAllowedDomain
{
    public int CampusAllowedDomainId { get; set; }
    public int CampusId { get; set; }
    public string Domain { get; set; } = null!;
    public DateTime CreatedAt { get; set; }

    // Navigation property
    public Campus Campus { get; set; } = null!;
}