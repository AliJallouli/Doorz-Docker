using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Invitation.Request.DTOs;

public class CreateInvitationRequestDto
{
    [Required(ErrorMessage = "Le type d'entité est requis.")]
    public string EntityTypeName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le nom est requis.")]
    [StringLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "L'email est requis.")]
    [EmailAddress(ErrorMessage = "L'email doit être valide.")]
    [StringLength(191, ErrorMessage = "L'email ne peut pas dépasser 191 caractères.")]
    public string InvitationEmail { get; set; } = string.Empty;

    
    [RegularExpression(@"^\d{4}\.\d{3}\.\d{3}$", 
        ErrorMessage = "Le numéro d'entreprise doit suivre le format XXXX.XXX.XXX.")]
    public string? CompanyNumber { get; set; }

    public int? InstitutionTypeId { get; set; }
    public int? LanguageId { get; set; }

}