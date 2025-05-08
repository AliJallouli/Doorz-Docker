namespace Domain.Entities.Legals;

public class LegalDocumentClause
{
    public int ClauseId { get; set; }
    public int DocumentId { get; set; }
    public int OrderIndex { get; set; }

    public LegalDocument? Document { get; set; }
    public ICollection<LegalDocumentClauseTranslation> Translations { get; set; } = new List<LegalDocumentClauseTranslation>();

}