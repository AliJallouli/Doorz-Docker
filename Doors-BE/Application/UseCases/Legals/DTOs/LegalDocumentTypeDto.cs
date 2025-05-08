namespace Application.UseCases.Legals.DTOs;

public class LegalDocumentTypeDto
{
    public int DocumentTypeId { get; set; }
    public string Name { get; set; } =string.Empty;            
    public string Label { get; set; }  =string.Empty;         
    public string Description { get; set; }   =string.Empty;   
    public string LanguageCode { get; set; }  =string.Empty;  
}