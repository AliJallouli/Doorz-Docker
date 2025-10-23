using Domain.Entities.References;

namespace Domain.Entities;

public class StudentMovement
{
    public int StudentMovementId { get; set; }
    public int? EventOwnerId { get; set; }
    public int? HousingOwnerId { get; set; }
    public int EntityId { get; set; }

    public string Name { get; set; } = string.Empty;
    public string? Acronym { get; set; }
    public string? EnterpriseNumber { get; set; }
    public string? Description { get; set; }
    public string? Website { get; set; }
    public string? Logo { get; set; }
    public DateTime? FoundingDate { get; set; }
    public int? MemberCount { get; set; }

    public bool IsActive { get; set; }
    public int LikeCount { get; set; }
    public int VisitCount { get; set; }

    public int? ContactId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }

    // Propriétés de navigation
    public Entity Entity { get; set; }= null!;
    public EventOwner? EventOwner { get; set; }
    public HousingOwner? HousingOwner { get; set; }
    public Contact? Contact { get; set; }
    public Users? Creator { get; set; }
    public Users? Updater { get; set; }
    public ICollection<Offer>? Offers { get; set; }= new List<Offer>();
}