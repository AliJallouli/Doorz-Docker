namespace Application.UseCases.Legals.DTOs;

public class LegalClauseDto
{
    public int ClauseId { get; set; }
    public int OrderIndex { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty; 
}