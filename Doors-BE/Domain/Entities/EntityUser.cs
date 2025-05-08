namespace Domain.Entities;

public class EntityUser:IHasCreatedAt
{
    public int EntityUserId { get; set; } // Identifiant unique de l’association
    public int UserId { get; set; } // Référence à l’utilisateur (users.user_id)

    public int
        EntityId { get; set; } // Référence à l’entité (institution_id, company_id, student_id, landlord_id, etc.)

    public int RoleId { get; set; } // Rôle de l’utilisateur dans cette entité (Admin, Student, etc.)
    public DateTime CreatedAt { get; set; } // Date de création de l’association

    // Propriétés de navigation (optionnelles, selon vos besoins)
    public Users User { get; set; } = null!; // Navigation vers l’utilisateur
    public Entity Entity { get; set; } = null!; // Navigation vers le type d’entité
    public Role Role { get; set; }= null!;
}