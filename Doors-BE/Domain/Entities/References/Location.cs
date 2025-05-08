namespace Domain.Entities.References;

public class Location
{
    public int LocationId { get; set; }
    public string Street { get; set; } = null!;
    public string Number { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Country { get; set; } = null!;
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }

    // Navigation properties
    public Users? Creator { get; set; }
    public Users? Updater { get; set; }
    public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
    public ICollection<Housing> Housings { get; set; } = new List<Housing>();
    public ICollection<Offer> Offers { get; set; } = new List<Offer>();
    public ICollection<Event> Events { get; set; } = new List<Event>();
}