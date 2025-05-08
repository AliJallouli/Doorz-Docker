namespace Domain.Entities.References;

public class PaymentLog
{
    public int PaymentLogId { get; set; }
    public int PaymentId { get; set; }
    public int? PreviousStatusId { get; set; }
    public int NewStatusId { get; set; }
    public DateTime LogDate { get; set; }
    public string? Details { get; set; }
    public int? CreatedBy { get; set; }

    // Navigation properties
    public Payment Payment { get; set; } = null!;
    public PaymentStatus? PreviousStatus { get; set; }
    public PaymentStatus NewStatus { get; set; } = null!;
    public Users? Creator { get; set; }
}