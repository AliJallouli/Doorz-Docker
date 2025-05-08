using Domain.Entities.References;
namespace Domain.Entities;

public class Landlord:IHasUpdatedAt,IHasCreatedAt
{
    public int LandlordId { get; set; }
    public int EntityId { get; set; }
    public int? HousingOwnerId { get; set; }
    public int? ContactId { get; set; }
    public int HousingCount { get; set; }
    public decimal? Rating { get; set; } // DECIMAL(3,2)
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Propriétés de navigation
    public Entity Entity { get; set; } = null!;
    public HousingOwner? HousingOwner { get; set; }
    public Contact? Contact { get; set; }
}