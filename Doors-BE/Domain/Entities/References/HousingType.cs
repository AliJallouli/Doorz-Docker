namespace Domain.Entities.References;

public class HousingType
{
    public int HousingTypeId { get; set; }
    public string Name { get; set; } = null!;

    // Navigation property
    public ICollection<Housing> Housings { get; set; } = new List<Housing>();
}