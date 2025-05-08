using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Auth.DTOs;

public class LoginRequestDto
{
    [Required(ErrorMessage = "L'email est requis.")]
    [EmailAddress(ErrorMessage = "L'email doit être dans un format valide (ex. utilisateur@domaine.com).")]
    [StringLength(191, MinimumLength = 4, ErrorMessage = "L'email doit contenir entre 3 et 254 caractères.")]
    public string Email { get; set; } = null!;


    public string Password { get; set; } = null!;
}