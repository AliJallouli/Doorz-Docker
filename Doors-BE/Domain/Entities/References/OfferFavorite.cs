namespace Domain.Entities.References;

public class OfferFavorite
{
    public int OfferFavoriteId { get; set; }
    public int StudentId { get; set; }
    public int OfferId { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public Student Student { get; set; } = null!;
    public Offer Offer { get; set; } = null!;
}