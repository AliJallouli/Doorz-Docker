namespace Domain.Entities.References;

public class StudentReferral
{
    public int StudentReferralId { get; set; }
    public int ReferringStudentId { get; set; }
    public int ReferredStudentId { get; set; }
    public string EntityType { get; set; } = null!;
    public int EntityId { get; set; }
    public decimal? Reward { get; set; }
    public string ReferralStatus { get; set; } = "Pending";
    public string? Message { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }

    // Navigation properties
    public Student ReferringStudent { get; set; } = null!;
    public Student ReferredStudent { get; set; } = null!;
    public Users? Creator { get; set; }
    public Users? Updater { get; set; }
}