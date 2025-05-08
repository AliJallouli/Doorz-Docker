using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Invitation.SuperAdmin.DTOs;

public class CreateInstitutionDto
{
    public string Name { get; set; } = string.Empty;


    public int InstitutionTypeId { get; set; }

    [Required(ErrorMessage = "L'email est requis.")]
    [EmailAddress(ErrorMessage = "L'email doit être dans un format valide (ex. utilisateur@domaine.com).")]
    [StringLength(191, MinimumLength = 4, ErrorMessage = "L'email doit contenir entre 3 et 254 caractères.")]
    public string InvitationEmail { get; set; } = string.Empty;
}