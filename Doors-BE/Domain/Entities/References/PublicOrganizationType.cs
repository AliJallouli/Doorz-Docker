using Domain.Entities.Translations;

namespace Domain.Entities.References;

public class PublicOrganizationType
{
    public int PublicOrganizationTypeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<PublicOrganizationTypeTranslation> Translations { get; set; } = new List<PublicOrganizationTypeTranslation>();
    public ICollection<PublicOrganization> PublicOrganizations { get; set; } = new List<PublicOrganization>();
}