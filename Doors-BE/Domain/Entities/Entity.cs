namespace Domain.Entities;

public class Entity:IHasUpdatedAt,IHasCreatedAt
{
    public int EntityId { get; set; }
    public int EntityTypeId { get; set; }
    public int SpecificEntityId { get; set; }
    public string Name { get; set; } // VARCHAR(191), requis
    public int? ParentEntityId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime  UpdatedAt { get; set; }
    public int? CreatedBy { get; set; }

    // Propriétés de navigation
    public EntityType EntityType { get; set; }
    public Entity? ParentEntity { get; set; }
    public Users? CreatedByUser { get; set; }
    public ICollection<Entity> ChildEntities { get; set; } = new List<Entity>();
    public ICollection<EntityUser> EntityUsers { get; set; } = new HashSet<EntityUser>();
}