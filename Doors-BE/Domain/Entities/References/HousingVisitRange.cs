namespace Domain.Entities.References;

public class HousingVisitRange
{
    public int HousingVisitRangeId { get; set; }
    public int HousingVisitId { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public string? Message { get; set; } // TEXT
    public string RangeStatus { get; set; } // VARCHAR(20), requis (Enum à définir)
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Propriété de navigation
    public HousingVisit HousingVisit { get; set; }
}

// Enum à définir pour RangeStatus