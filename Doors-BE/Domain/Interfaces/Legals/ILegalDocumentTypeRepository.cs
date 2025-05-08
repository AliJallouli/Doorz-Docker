using Domain.Entities.Legals;

namespace Domain.Interfaces.Legals;

public interface ILegalDocumentTypeRepository
{
    Task<LegalDocumentType?> GetByNameAsync(string name);
    Task<List<LegalDocumentType>> GetAllAsync();
    Task AddAsync(LegalDocumentType entity);
    Task<List<LegalDocumentType>> GetAllWithTranslationAsync(string languageCode);
}