using Domain.Entities.References;
namespace Domain.Entities;

public class Campus:IHasUpdatedAt,IHasCreatedAt
{
    public int CampusId { get; set; }
    public int EntityId { get; set; }
    public int? EventOwnerId { get; set; }
    public int? HousingOwnerId { get; set; }
    public string Name { get; set; } = null!; // VARCHAR(50), requis
    public string? Acronym { get; set; } // VARCHAR(10)
    public string? Description { get; set; } // TEXT
    public string? OfficialCode { get; set; } // VARCHAR(20)
    public DateTime? OpeningDate { get; set; }
    public int? Capacity { get; set; }
    public decimal? Area { get; set; } // DECIMAL(10,2)
    public int LikeCount { get; set; } 
    public decimal? Rating { get; set; } // DECIMAL(3,2)
    public int VisitCount { get; set; } 
    public string? Logo { get; set; } // VARCHAR(191)
    public bool UseInstitutionData { get; set; } = true;
    public int? CampusTypeId { get; set; }
    public bool IsActive { get; set; } = true;
    public int? ContactId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }

    // Propriétés de navigation
    public Entity Entity { get; set; } = null!;
    public EventOwner? EventOwner { get; set; }
    public HousingOwner? HousingOwner { get; set; }
    public CampusType? CampusType { get; set; }
    public Contact? Contact { get; set; }
    public Users? Creator { get; set; }
    public Users? Updater { get; set; }
    public ICollection<Degree>? Degrees { get; set; }
    public ICollection<CampusFacility>? CampusFacilities { get; set; }
    public ICollection<InstitutionCampusInvitation>? InstitutionCampusInvitations { get; set; }
    public ICollection<CampusAllowedDomain>? CampusAllowedDomains { get; set; }
}