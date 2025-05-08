namespace Domain.Entities.References;

public class HousingOwner : IHousingOwner
{
    public int HousingOwnerId { get; set; }
    public string OwnerType { get; set; } = null!;
    public DateTime CreatedAt { get; set; }

    // Navigation property
    public ICollection<Housing> Housings { get; set; } = new List<Housing>();
}