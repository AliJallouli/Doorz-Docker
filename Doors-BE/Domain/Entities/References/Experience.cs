namespace Domain.Entities.References;

public class Experience
{
    public int ExperienceId { get; set; }
    public int CvId { get; set; }
    public string JobTitle { get; set; } = null!;
    public string Company { get; set; } = null!;
    public string? Location { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Description { get; set; }

    // Navigation property
    public CV CV { get; set; } = null!;
}