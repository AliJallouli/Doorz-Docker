using Domain.Entities.Translations;

namespace Domain.Entities;

public class EntityType:IHasCreatedAt
{
    public int EntityTypeId { get; set; }
    public string Name { get; set; } = null!;// Non nullable, UNIQUE
    public string? Description { get; set; } // Nullable
    public DateTime CreatedAt { get; set; }

    // Propriété de navigation : Liste des rôles associés
    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
    public ICollection<Entity> Entities { get; set; } = new HashSet<Entity>();
    public ICollection<EntityTypeTranslation>? Translations { get; set; } // Relation avec les traductions
}