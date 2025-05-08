using Domain.Entities.Translations;

namespace Domain.Entities;

public class Role:IHasUpdatedAt,IHasCreatedAt
{
    public int RoleId { get; set; }
    public string Name { get; set; } = null!;
    public int EntityTypeId { get; set; }

    public string?
        Description
    {
        get;
        set;
    } // Description par défaut (ex. "Administrateur de l’entreprise avec tous les privilèges")

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Propriété de navigation : Référence au type d'entité
    public virtual EntityType EntityType { get; set; } = null!;
    public ICollection<EntityUser>? EntityUsers { get; set; } = new HashSet<EntityUser>();
    public ICollection<RoleTranslation>? Translations { get; set; } // Relation avec les traductions
}