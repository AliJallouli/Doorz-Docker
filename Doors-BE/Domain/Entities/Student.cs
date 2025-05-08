using Domain.Entities.References;
namespace Domain.Entities;

public class Student:IHasUpdatedAt,IHasCreatedAt
{
    public int StudentId { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? Bio { get; set; } // TEXT
    public string? Linkedin { get; set; } // VARCHAR(191)
    public string? Github { get; set; } // VARCHAR(191)
    public string? Portfolio { get; set; } // VARCHAR(191)
    public int? ExpectedGraduationYear { get; set; }
    public PreferredJobType? PreferredJobType { get; set; } // Enum à définir
    public string? PreferredLocation { get; set; } // VARCHAR(100)
    public bool NotificationEnabled { get; set; } = true;
    public string? StudyField { get; set; } // VARCHAR(100)
    public string? CvPath { get; set; } // VARCHAR(191)
    public int? StudyLevelId { get; set; }
    public int? ContactId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public int EntityId { get; set; }

    // Propriétés de navigation
    public Entity Entity { get; set; } = null!;
    public StudyLevel? StudyLevel { get; set; }
    public Contact? Contact { get; set; }
    public ICollection<CV>? CVs { get; set; }
    public ICollection<HousingApplication>? HousingApplications { get; set; }
    public ICollection<HousingVisit>? HousingVisits { get; set; }
    public ICollection<Application>? Applications { get; set; }
    public ICollection<OfferFavorite>? OfferFavorites { get; set; }
    public ICollection<WorkHours>? WorkHours { get; set; }
    public ICollection<EntityReview>? EntityReviews { get; set; }
    public ICollection<StudentReferral>? ReferralsAsReferring { get; set; }
    public ICollection<StudentReferral>? ReferralsAsReferred { get; set; }
    public ICollection<EventInterest>? EventInterests { get; set; }
    public ICollection<EntityLike>? EntityLikes { get; set; }
}