using Domain.Entities.References;
namespace Domain.Entities;

public class Institution
{
    public int InstitutionId { get; set; }
    public int EntityId { get; set; } 
    public int? EventOwnerId { get; set; }
    public string Name { get; set; }= String.Empty; // VARCHAR(50), requis
    public string? Acronym { get; set; } // VARCHAR(10)
    public string? Description { get; set; } // TEXT
    public string? Website { get; set; } // VARCHAR(191)
    public string? Logo { get; set; } // VARCHAR(191)
    public int InstitutionTypeId { get; set; }
    public int? CommunityId { get; set; }
    public int? LegalStatusId { get; set; }
    public int? NetworkId { get; set; }
    public string? OfficialCode { get; set; } // VARCHAR(20)
    public DateTime? FoundingDate { get; set; }
    public int? StudentCount { get; set; }
    public bool IsOfficiallyRecognized { get; set; } = true;
    public int? AuthorityId { get; set; }
    public int? EducationLevelId { get; set; }
    public bool IsModular { get; set; }
    public string? TargetAudience { get; set; } // VARCHAR(50)
    public decimal? AverageTuitionFee { get; set; } // DECIMAL(10,2)
    public int LikeCount { get; set; }
    public int VisitCount { get; set; } 
    public int? ContactId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }

    // Propriétés de navigation
    public Entity Entity { get; set; } = null!;
    public EventOwner? EventOwner { get; set; }
    public InstitutionType InstitutionType { get; set; } = null!;
    public Community? Community { get; set; }
    public LegalStatus? LegalStatus { get; set; }
    public Network? Network { get; set; }
    public Authority? Authority { get; set; }
    public EducationLevel? EducationLevel { get; set; }
    public Contact? Contact { get; set; }
    public Users? Creator { get; set; }
    public Users? Updater { get; set; }
    public ICollection<Campus>? Campuses { get; set; } = new List<Campus>();
    public ICollection<InstitutionInvitation>? InstitutionInvitations { get; set; }= new List<InstitutionInvitation>();
    public ICollection<InstitutionCampusInvitation>? InstitutionCampusInvitations { get; set; }= new List<InstitutionCampusInvitation>();
}