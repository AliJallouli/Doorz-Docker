namespace Domain.Entities.References;

public class HousingApplication
{
    public int HousingApplicationId { get; set; }
    public int StudentId { get; set; }
    public int HousingId { get; set; }
    public int? ApplicationStatusId { get; set; }
    public string? Message { get; set; }
    public DateTime AppliedAt { get; set; }

    // Navigation properties
    public Student Student { get; set; } = null!;
    public Housing Housing { get; set; } = null!;
    public ApplicationStatus? ApplicationStatus { get; set; }
}