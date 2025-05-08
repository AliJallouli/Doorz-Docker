namespace Domain.Entities.References;

public class PaymentItemEntity
{
    public int PaymentItemEntityId { get; set; }
    public int PaymentItemId { get; set; }
    public string EntityType { get; set; } = null!;
    public int EntityId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Navigation property
    public PaymentItem PaymentItem { get; set; } = null!;
}