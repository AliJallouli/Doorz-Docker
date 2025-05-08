using Domain.Entities.References;
namespace Domain.Entities;

public class Users:IHasUpdatedAt,IHasCreatedAt
{
    public int UserId { get; set; }
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int SuperRoleId { get; set; }
    public bool IsVerified { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }

    // Navigation properties
    public SuperRole SuperRole { get; set; } = null!;
    public Users? Creator { get; set; }
    public Users? Updater { get; set; }
    public ICollection<LoginAttempt> LoginAttempts { get; set; } = new List<LoginAttempt>();
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    // Navigation inverse pour la relation de créateur
    public ICollection<Institution> CreatedInstitutions { get; set; } = new List<Institution>();

    // Navigation inverse pour la relation de modificateur (updater)
    public ICollection<Institution> UpdatedInstitutions { get; set; } = new List<Institution>();

    // Propriété de navigation vers les tentatives de connexion
    public EntityUser? EntityUser { get; set; }
    public ICollection<SessionEvent> SessionEvents { get; set; } = new List<SessionEvent>();
}