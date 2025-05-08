using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Auth.DTOs;

public class LegalConsentDto
{
    [Required(ErrorMessage = "L'identifiant du document légal est requis.")]
    [Range(1, int.MaxValue, ErrorMessage = "L'identifiant du document doit être un nombre positif.")]
    public int DocumentId { get; set; }
}