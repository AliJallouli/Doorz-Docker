namespace Domain.Entities.References;

public class CompanyType
{
    public int CompanyTypeId { get; set; }
    public string Name { get; set; } = null!;
    public string? Acronym { get; set; }
    public string? Description { get; set; }

    // Navigation property
    public ICollection<Company> Companies { get; set; } = new List<Company>();
}