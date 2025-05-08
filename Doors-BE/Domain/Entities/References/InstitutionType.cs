using Domain.Entities.Translations;

namespace Domain.Entities.References;

public class InstitutionType
{
    public int InstitutionTypeId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    // Navigation property
    public ICollection<Institution> Institutions { get; set; } = new List<Institution>();

    public ICollection<InstitutionTypeTranslation> Translations { get; set; } =
        new HashSet<InstitutionTypeTranslation>();
}