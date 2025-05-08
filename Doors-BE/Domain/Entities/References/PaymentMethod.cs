namespace Domain.Entities.References;

public class PaymentMethod
{
    public int PaymentMethodId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Navigation property
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}