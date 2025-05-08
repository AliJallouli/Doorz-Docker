namespace Application.UseCases.References.Language.DTOs;

public class SpokenLanguageDto
{
    public int LanguageId { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
}