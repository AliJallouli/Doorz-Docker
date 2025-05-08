namespace Domain.Entities.References;

public class WorkHours
{
    public int WorkHourId { get; set; }
    public int StudentId { get; set; }
    public int OfferId { get; set; }
    public int EmployerId { get; set; }
    public decimal HoursWorked { get; set; }
    public DateTime WorkDate { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public Student Student { get; set; } = null!;
    public Offer Offer { get; set; } = null!;
    public Company Employer { get; set; } = null!;
}