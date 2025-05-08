namespace Domain.Entities;

public class InvitationType:IHasCreatedAt
{
    public int InvitationTypeId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation
    public ICollection<SuperadminInvitation>? Invitations { get; set; }
}