namespace Domain.Entities.References;

public class CVSkill
{
    public int CvSkillId { get; set; }
    public int CvId { get; set; }
    public string Name { get; set; } = null!;
    public string Proficiency { get; set; } = "IntermÃ©diaire";

    // Navigation property
    public CV CV { get; set; } = null!;
}