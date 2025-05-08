using Domain.Entities.References;
namespace Domain.Entities.Legals;

public class LegalDocumentClauseTranslation
{
    public int TranslationId { get; set; }
    public int ClauseId { get; set; }
    public int LanguageId { get; set; }
    public string? Title { get; set; }
    public string Content { get; set; } = string.Empty;

    public LegalDocumentClause? Clause { get; set; }
    public SpokenLanguage? Language { get; set; }
}