using Domain.Entities.References;

namespace Domain.Entities.Translations;

public class PublicOrganizationTypeTranslation
{
    public int PublicOrganizationTypeTranslationId { get; set; }
    public int PublicOrganizationTypeId { get; set; }
    public int LanguageId { get; set; }

    public string TranslatedName { get; set; } = string.Empty;
    public string? TranslatedDescription { get; set; }

    public PublicOrganizationType PublicOrganizationType { get; set; } = null!;
    public SpokenLanguage Language { get; set; } = null!;
}