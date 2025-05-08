namespace Domain.Entities.References;

public class OwnerType
{
    public int OwnerTypeId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Navigation property
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}