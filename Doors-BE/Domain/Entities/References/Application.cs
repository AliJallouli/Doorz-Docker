namespace Domain.Entities.References;

public class Application
{
    public int ApplicationId { get; set; }
    public int? StudentId { get; set; }
    public int? OfferId { get; set; }
    public int? ApplicationStatusId { get; set; }
    public string? CvPath { get; set; }
    public string? CoverLetterPath { get; set; }
    public string? Reason { get; set; }
    public bool ViewedByCompany { get; set; }
    public DateTime AppliedAt { get; set; }

    // Navigation properties
    public Student? Student { get; set; }
    public Offer? Offer { get; set; }
    public ApplicationStatus? ApplicationStatus { get; set; }
}