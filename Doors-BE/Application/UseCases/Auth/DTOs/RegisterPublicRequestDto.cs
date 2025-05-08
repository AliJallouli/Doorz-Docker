using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Auth.DTOs;

public class RegisterPublicRequestDto
{
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Le prénom doit contenir entre 2 et 50 caractères.")]
    public string? FirstName { get; set; }

    [StringLength(50, MinimumLength = 2, ErrorMessage = "Le nom de famille doit contenir entre 2 et 50 caractères.")]
    public string? LastName { get; set; }


    [Required(ErrorMessage = "L'email est requis.")]
    [EmailAddress(ErrorMessage = "L'email doit être dans un format valide (ex. utilisateur@domaine.com).")]
    [StringLength(191, MinimumLength = 4, ErrorMessage = "L'email doit contenir entre 3 et 254 caractères.")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "L'identifiant du rôle est requis.")]
    [Range(1, int.MaxValue, ErrorMessage = "L'identifiant du rôle doit être un nombre positif.")]
    public int RoleId { get; set; }
    
    [Required(ErrorMessage = "Le mot de passe est requis.")]
    [MinLength(6, ErrorMessage = "Le mot de passe doit contenir au moins 6 caractères.")]
    [StringLength(100, ErrorMessage = "Le mot de passe ne peut pas dépasser 100 caractères.")]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d).+$",
        ErrorMessage = "Le mot de passe doit contenir au moins une lettre et un chiffre.")]
    public string Password { get; set; } = null!;
    [Required(ErrorMessage = "Les consentements légaux sont requis.")]
    [MinLength(3, ErrorMessage = "Les trois consentements légaux doivent être fournis.")]
    public List<LegalConsentDto> LegalConsents { get; set; } = new List<LegalConsentDto>();
}