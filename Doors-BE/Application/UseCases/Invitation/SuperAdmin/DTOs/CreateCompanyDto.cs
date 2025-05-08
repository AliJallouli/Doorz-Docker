using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Invitation.SuperAdmin.DTOs;

public class CreateCompanyDto
{
  
    public string Name { get; set; } = string.Empty;

    [EmailAddress(ErrorMessage = "L'email doit être dans un format valide (ex. utilisateur@domaine.com).")]
    [StringLength(191, MinimumLength = 4, ErrorMessage = "L'email doit contenir entre 3 et 254 caractères.")]
    public string InvitationEmail { get; set; } = string.Empty;

    [RegularExpression(@"^\d{4}\.\d{3}\.\d{3}$",
        ErrorMessage = "Le numéro d'entreprise belge doit suivre le format XXXX.XXX.XXX (10 chiffres avec points).")]
    public string CompanyNumber { get; set; }=string.Empty;
}