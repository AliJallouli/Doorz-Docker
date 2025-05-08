namespace Domain.Entities.References;

public class Tax
{
    public int TaxId { get; set; }
    public string Name { get; set; } = null!;
    public decimal Rate { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Navigation property
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}