namespace Domain.Entities.References;

public class StudyLevel
{
    public int StudyLevelId { get; set; }
    public string Name { get; set; } = null!;
    public int Year { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }

    // Navigation properties
    public Users? Creator { get; set; }
    public Users? Updater { get; set; }
    public ICollection<Student> Students { get; set; } = new List<Student>();
}