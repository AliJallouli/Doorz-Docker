namespace Domain.Entities.References;

public class MimeType
{
    public int MimeTypeId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    // Navigation property
    public ICollection<Image> Images { get; set; } = new List<Image>();
}