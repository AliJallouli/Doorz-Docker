namespace Application.UseCases.Invitation.Request.DTOs;

public class InvitationRequestDto
{
    public int InvitationRequestId { get; set; }
    
    public int EntityTypeId { get; set; }
    public string EntityTypeName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string InvitationEmail { get; set; } = string.Empty;

    public string? CompanyNumber { get; set; }
    public int? InstitutionTypeId { get; set; }
    public string? InstitutionTypeName { get; set; } 

    public string Status { get; set; } = "PENDING";
    public string? RejectionReason { get; set; }

    public string? SubmittedIp { get; set; }
    public string? UserAgent { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}