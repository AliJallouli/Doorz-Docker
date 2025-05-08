namespace Domain.Entities.References;

public class EntityReview
{
    public int EntityReviewId { get; set; }
    public int StudentId { get; set; }
    public string EntityType { get; set; } = null!;
    public int EntityId { get; set; }
    public decimal Rating { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }

    // Navigation properties
    public Student Student { get; set; } = null!;
    public Users? Creator { get; set; }
    public Users? Updater { get; set; }
}