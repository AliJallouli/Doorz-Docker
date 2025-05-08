using Domain.Entities.References;

namespace Domain.Entities.Translations;

public class EntityTypeTranslation
{
    public int EntityTypeTranslationId { get; set; }
    public int EntityTypeId { get; set; }
    public int LanguageId { get; set; }
    public string TranslatedName { get; set; } = null!;
    public string? TranslatedDescription { get; set; }

    public EntityType EntityType { get; set; } = null!;
    public SpokenLanguage Language { get; set; } = null!;
}