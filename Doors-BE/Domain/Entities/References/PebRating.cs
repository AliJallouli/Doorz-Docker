namespace Domain.Entities.References;

public class PebRating
{
    public int PebRatingId { get; set; }
    public string Name { get; set; } // VARCHAR(10), requis

    // Propriété de navigation
    public ICollection<Housing> Housings { get; set; }
}