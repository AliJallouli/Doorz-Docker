using Domain.Entities.References;
namespace Domain.Entities;

public class Company:IHasUpdatedAt,IHasCreatedAt
{
    public int CompanyId { get; set; }
    public int EntityId { get; set; }
    public int? EventOwnerId { get; set; }
    public int? HousingOwnerId { get; set; }
    public string Name { get; set; } = null!; // VARCHAR(100), requis
    public string? Acronym { get; set; } // VARCHAR(10)
    public string CompanyNumber { get; set; } = null!; // VARCHAR(12)
    public string? Sector { get; set; } // VARCHAR(50)
    public string? Website { get; set; } // VARCHAR(191)
    public string? Description { get; set; } // TEXT
    public int? CollaboratorCount { get; set; }
    public string? Logo { get; set; } // VARCHAR(191)
    public int? ResponsibleUserId { get; set; }
    public int? CompanyTypeId { get; set; }
    public int? Capacity { get; set; }
    public DateTime? FoundingDate { get; set; }
    public bool IsActive { get; set; } = true;
    public int LikeCount { get; set; } 
    public decimal? Rating { get; set; } // DECIMAL(3,2)
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
    public Users? ResponsibleUser { get; set; }
    public CompanyType? CompanyType { get; set; }
    public Contact? Contact { get; set; }
    public Users? Creator { get; set; }
    public Users? Updater { get; set; }
    public ICollection<Offer>? Offers { get; set; }= new List<Offer>();
    public ICollection<DegreePartnership>? DegreePartnerships { get; set; }= new List<DegreePartnership>();
}