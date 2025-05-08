using Domain.Entities.References;

namespace Domain.Entities.Translations;

public class InstitutionTypeTranslation
{
    public int InstitutionTypeTranslationId { get; set; }
    public int InstitutionTypeId { get; set; }
    public int LanguageId { get; set; }
    public string TranslatedName { get; set; } = null!;
    public string? TranslatedDescription { get; set; }

    public virtual InstitutionType InstitutionType { get; set; } = null!;
    public virtual SpokenLanguage Language { get; set; } = null!;
}