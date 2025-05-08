using Domain.Entities.References;

namespace Domain.Entities.Translations;

public class RoleTranslation
{
    public int RoleTranslationId { get; set; }
    public int RoleId { get; set; }
    public int LanguageId { get; set; }
    public string TranslatedName { get; set; }
    public string TranslatedDescription { get; set; }
    public Role Role { get; set; } = null!;
    public SpokenLanguage Language { get; set; } = null!;
}