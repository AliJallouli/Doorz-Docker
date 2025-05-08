using Domain.Entities.Legals;

namespace Domain.Interfaces.Legals;

public interface ILegalDocumentTypeTranslationRepository
{
    Task<LegalDocumentTypeTranslation?> GetByTypeAndLanguageAsync(int typeId, int languageId);
    Task<LegalDocumentTypeTranslation?> GetByTypeIdAndLanguageAsync(int documentTypeId, int languageId);
}