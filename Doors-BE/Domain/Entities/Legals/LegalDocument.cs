namespace Domain.Entities.Legals;
public class LegalDocument
{
    public int DocumentId { get; set; }

    public int DocumentTypeId { get; set; }
    public LegalDocumentType DocumentType { get; set; } = null!;

    public string Version { get; set; } = null!;
    public DateTime PublishedAt { get; set; }
    public DateTime? ValidUntil { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<LegalDocumentClause> Clauses { get; set; } = new List<LegalDocumentClause>();
    public ICollection<UserLegalConsent> Consents { get; set; } = new List<UserLegalConsent>();
}