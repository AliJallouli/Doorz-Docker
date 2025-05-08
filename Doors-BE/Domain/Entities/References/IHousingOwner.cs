namespace Domain.Entities.References;

public interface IHousingOwner
{
    int HousingOwnerId { get; set; }
    string OwnerType { get; set; }
    DateTime CreatedAt { get; set; }
    ICollection<Housing> Housings { get; set; }
}