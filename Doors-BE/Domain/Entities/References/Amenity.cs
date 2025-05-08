namespace Domain.Entities.References;
public class Amenity
{
    public int AmenityId { get; set; }
    public string Name { get; set; }= String.Empty; // VARCHAR(50), requis
    public string? Description { get; set; } // NULLABLE

    // Propriété de navigation
    public ICollection<HousingAmenity> HousingAmenities { get; set; } = new List<HousingAmenity>();
}