namespace Domain.Entities.References;

public class CV
{
    public int CvId { get; set; }
    public int StudentId { get; set; }
    public string Title { get; set; } = null!;
    public string? Objective { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }
    public bool IsDefault { get; set; }

    // Navigation properties
    public Student Student { get; set; } = null!;
    public ICollection<Experience> Experiences { get; set; } = new List<Experience>();
    public ICollection<Education> Educations { get; set; } = new List<Education>();
    public ICollection<CVSkill> Skills { get; set; } = new List<CVSkill>();
    public ICollection<CVLanguage> Languages { get; set; } = new List<CVLanguage>();
}