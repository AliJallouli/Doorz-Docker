namespace Application.UseCases.Legals.DTOs;

public class LegalDocumentDto
{
    public int DocumentId { get; set; }
    public string Version { get; set; }=string.Empty;
    public DateTime PublishedAt { get; set; }
    public bool IsActive { get; set; }
    
    public string DocumentTypeName { get; set; }=string.Empty;
    public string DocumentTypeLabel { get; set; } =string.Empty;
    public string LanguageCode { get; set; }=string.Empty;

    public List<LegalClauseDto> Clauses { get; set; } = new();
}