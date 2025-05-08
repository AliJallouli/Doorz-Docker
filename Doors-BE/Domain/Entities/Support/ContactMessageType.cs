namespace Domain.Entities.Support;

public class ContactMessageType
{
    public int ContactMessageTypeId { get; set; }

    public int Priority { get; set; } = 0; 
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }
   public string KeyName { get; set; } = null!;

    // Relations
    public ICollection<ContactMessageTypeTranslation> Translations { get; set; } = new List<ContactMessageTypeTranslation>();

    public ICollection<ContactMessage> ContactMessages { get; set; } = new List<ContactMessage>();
}