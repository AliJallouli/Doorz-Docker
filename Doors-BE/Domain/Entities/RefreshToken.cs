namespace Domain.Entities;

public class RefreshToken:IHasCreatedAt
{
    public int RefreshTokenId { get; set; }
    public int UserId { get; set; }
    public string Token { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }
    public bool Used { get; set; } = false;
    public DateTime? UsedAt { get; set; }


    // Clé étrangère vers l'événement de session (correspond à sessionEvent_id dans la table)
    public int SessionEventId { get; set; }

    // Navigation property
    public Users Users { get; set; } = null!;

    // Navigation property vers l'événement de session
    public SessionEvent SessionEvent { get; set; } = null!;
}