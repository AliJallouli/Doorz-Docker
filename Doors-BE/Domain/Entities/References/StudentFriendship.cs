namespace Domain.Entities.References;
public class StudentFriendship
{
    public int StudentFriendshipId { get; set; }
    public int StudentId1 { get; set; }
    public int StudentId2 { get; set; }
    public string Status { get; set; } // VARCHAR(20), requis (Enum à définir)
    public DateTime RequestDate { get; set; }
    public DateTime? ResponseDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }

    // Propriétés de navigation
    public Student Student1 { get; set; }
    public Student Student2 { get; set; }
    public Users? Creator { get; set; }
    public Users? Updater { get; set; }
}