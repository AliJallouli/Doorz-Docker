namespace Domain.Entities.References;

public class Bridge
{
    public int BridgeId { get; set; }
    public int? FromDegreeId { get; set; }
    public int? ToDegreeId { get; set; }
    public int? AdditionalCreditCount { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }

    // Navigation properties
    public Degree? FromDegree { get; set; }
    public Degree? ToDegree { get; set; }
    public Users? Creator { get; set; }
    public Users? Updater { get; set; }
}