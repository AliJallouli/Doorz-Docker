using Domain.Entities.References;

namespace Domain.Entities.Support;

public class ContactMessageTypeTranslation
{
    public int ContactMessageTypeTranslationId { get; set; }

    public int ContactMessageTypeId { get; set; }

    public ContactMessageType ContactMessageType { get; set; } = null!;

    public int LanguageId { get; set; }

    public SpokenLanguage Language { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; } 
}