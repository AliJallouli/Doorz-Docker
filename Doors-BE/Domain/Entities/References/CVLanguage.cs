namespace Domain.Entities.References;

public class CVLanguage
{
    public int CvLanguageId { get; set; }
    public int? LanguageId { get; set; }
    public int CvId { get; set; }
    public string Level { get; set; } = "B1";

    // Navigation properties
    public SpokenLanguage? Language { get; set; }
    public CV CV { get; set; } = null!;
}