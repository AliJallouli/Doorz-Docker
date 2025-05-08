namespace Domain.Entities.References;

public class PaymentItem
{
    public int PaymentItemId { get; set; }
    public int PaymentId { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Navigation properties
    public Payment Payment { get; set; } = null!;
    public ICollection<PaymentItemEntity> PaymentItemEntities { get; set; } = new List<PaymentItemEntity>();
}