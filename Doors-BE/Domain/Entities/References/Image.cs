namespace Domain.Entities.References;

public class Image
{
    public int ImageId { get; set; }
    public string EntityType { get; set; } = null!;
    public int EntityId { get; set; }
    public string ImagePath { get; set; } = null!;
    public int MimeTypeId { get; set; }
    public bool IsPrimary { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }

    // Navigation properties
    public MimeType MimeType { get; set; } = null!;
    public Users? Creator { get; set; }
    public Users? Updater { get; set; }
}