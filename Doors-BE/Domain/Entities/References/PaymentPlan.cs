namespace Domain.Entities.References;

public class PaymentPlan
{
    public int PaymentPlanId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int CurrencyId { get; set; }
    public int? DurationDays { get; set; }
    public bool IsRecurring { get; set; }
    public int? MaxOfferCount { get; set; }
    public int? MaxHousingCount { get; set; }
    public int? MaxEventCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }

    // Navigation properties
    public Currency Currency { get; set; } = null!;
    public Users? Creator { get; set; }
    public Users? Updater { get; set; }
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}