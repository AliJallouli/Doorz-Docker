using Domain.Entities.References;
using Domain.Enums;

namespace Domain.Entities;

public class InvitationRequest
{
    public int InvitationRequestId { get; set; }

    public int EntityTypeId { get; set; }
    public string Name { get; set; } = string.Empty;

    public string InvitationEmail { get; set; } = string.Empty;

    public string? CompanyNumber { get; set; }
    public int? InstitutionTypeId { get; set; }

    public InvitationRequestStatus Status { get; set; } = InvitationRequestStatus.PENDING;
    public string? RejectionReason { get; set; }

    public string? SubmittedIp { get; set; }
    public string? UserAgent { get; set; }
    
    public int? LanguageId  { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Navigation
    public SpokenLanguage? SpokenLanguage { get; set; }
    public EntityType EntityType { get; set; } = null!;
    public InstitutionType? InstitutionType { get; set; }
}