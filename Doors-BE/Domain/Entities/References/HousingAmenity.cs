namespace Domain.Entities.References;

public class HousingAmenity
{
    public int HousingId { get; set; }
    public int AmenityId { get; set; }

    // Navigation properties
    public Housing Housing { get; set; } = null!;
    public Amenity Amenity { get; set; } = null!;
}