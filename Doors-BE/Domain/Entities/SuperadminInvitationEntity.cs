namespace Domain.Entities;

public class SuperadminInvitationEntity:IHasCreatedAt
{
    public int SuperadminInvitationEntityId { get; set; }
    public int SuperadminInvitationId { get; set; }
    public int EntityId { get; set; }
    public int RoleId { get; set; }
    public DateTime CreatedAt { get; set; }

    // Propriétés de navigation
    public SuperadminInvitation SuperadminInvitation { get; set; }
    public Entity Entity { get; set; }
    public Role Role { get; set; }
}