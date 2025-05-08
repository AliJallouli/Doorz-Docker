using Domain.Entities.References;
namespace Domain.Entities.Legals;
public class LegalDocumentTypeTranslation
{
    public int TranslationId { get; set; }

    public int DocumentTypeId { get; set; }
    public LegalDocumentType DocumentType { get; set; } = null!;

    public int LanguageId { get; set; }
    public SpokenLanguage Language { get; set; } = null!;

    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}