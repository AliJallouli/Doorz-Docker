namespace Domain.Entities.References;

public class Refund
{
    public int RefundId { get; set; }
    public int PaymentId { get; set; }
    public decimal Amount { get; set; }
    public int CurrencyId { get; set; }
    public DateTime RefundDate { get; set; }
    public string? RefundReference { get; set; }
    public string? Reason { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }

    // Navigation properties
    public Payment Payment { get; set; } = null!;
    public Currency Currency { get; set; } = null!;
    public Users? Creator { get; set; }
    public Users? Updater { get; set; }
}