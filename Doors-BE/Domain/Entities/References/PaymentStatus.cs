namespace Domain.Entities.References;
public class PaymentStatus
{
    public int PaymentStatusId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Navigation properties
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    public ICollection<PaymentLog> PaymentLogsAsPrevious { get; set; } = new List<PaymentLog>();
    public ICollection<PaymentLog> PaymentLogsAsNew { get; set; } = new List<PaymentLog>();
}