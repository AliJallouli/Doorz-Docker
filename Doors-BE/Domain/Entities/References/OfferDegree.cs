namespace Domain.Entities.References;

public class OfferDegree
{
    public int OfferDegreeId { get; set; }
    public int OfferId { get; set; }
    public int DegreeId { get; set; }
    public bool Mandatory { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Navigation properties
    public Offer Offer { get; set; } = null!;
    public Degree Degree { get; set; } = null!;
}