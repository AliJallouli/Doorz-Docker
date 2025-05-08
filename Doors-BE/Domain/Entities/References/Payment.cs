namespace Domain.Entities.References;

public class Payment
{
    public int PaymentId { get; set; }
    public int PaymentPlanId { get; set; }
    public int OwnerTypeId { get; set; }
    public int OwnerId { get; set; }
    public decimal Amount { get; set; }
    public int? TaxId { get; set; }
    public decimal? TaxAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public int CurrencyId { get; set; }
    public DateTime PaymentDate { get; set; }
    public int PaymentMethodId { get; set; }
    public int PaymentStatusId { get; set; }
    public string? TransactionReference { get; set; }
    public string? ExternalToken { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }

    // Navigation properties
    public PaymentPlan PaymentPlan { get; set; } = null!;
    public OwnerType OwnerType { get; set; } = null!;
    public Tax? Tax { get; set; }
    public Currency Currency { get; set; } = null!;
    public PaymentMethod PaymentMethod { get; set; } = null!;
    public PaymentStatus PaymentStatus { get; set; } = null!;
    public Users? Creator { get; set; }
    public Users? Updater { get; set; }
    public ICollection<Housing> Housings { get; set; } = new List<Housing>();
    public ICollection<Offer> Offers { get; set; } = new List<Offer>();
    public ICollection<Event> Events { get; set; } = new List<Event>();
    public ICollection<PaymentLog> PaymentLogs { get; set; } = new List<PaymentLog>();
    public ICollection<PaymentItem> PaymentItems { get; set; } = new List<PaymentItem>();
    public ICollection<Refund> Refunds { get; set; } = new List<Refund>();
}