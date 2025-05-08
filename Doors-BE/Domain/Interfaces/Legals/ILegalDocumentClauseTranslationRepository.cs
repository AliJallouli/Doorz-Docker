using Domain.Entities.Legals;

namespace Domain.Interfaces.Legals;

public interface ILegalDocumentClauseTranslationRepository
{
    Task<List<LegalDocumentClauseTranslation>> GetByDocumentIdAndLanguageAsync(int documentId, int languageId);
}