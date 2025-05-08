namespace Domain.Entities.References;

public class Education
{
    public int EducationId { get; set; }
    public int CvId { get; set; }
    public string Degree { get; set; } = null!;
    public string Institution { get; set; } = null!;
    public string? Location { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Description { get; set; }

    // Navigation property
    public CV CV { get; set; } = null!;
}