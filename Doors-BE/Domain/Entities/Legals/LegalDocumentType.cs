namespace Domain.Entities.Legals;
public class LegalDocumentType
{
    public int DocumentTypeId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public ICollection<LegalDocumentTypeTranslation> Translations { get; set; } = new List<LegalDocumentTypeTranslation>();
    public ICollection<LegalDocument> Documents { get; set; } = new List<LegalDocument>();
}