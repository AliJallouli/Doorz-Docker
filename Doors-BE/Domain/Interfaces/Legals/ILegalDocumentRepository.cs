using Domain.Entities.Legals;

namespace Domain.Interfaces.Legals;

public interface ILegalDocumentRepository
{
    Task<LegalDocument?> GetActiveByTypeAsync(string typeName);
    Task<LegalDocument?> GetByIdAsync(int id);
    Task<List<LegalDocument>> GetAllActiveAsync();
    Task AddAsync(LegalDocument document);
    Task<LegalDocument?> GetActiveByTypeAndLanguageAsync(string typeName, string languageCode);
}