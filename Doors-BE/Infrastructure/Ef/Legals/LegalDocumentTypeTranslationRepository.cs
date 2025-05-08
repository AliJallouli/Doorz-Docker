using Domain.Entities.Legals;
using Domain.Interfaces.Legals;
using Infrastructure.Ef.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Ef.Legals;

public class LegalDocumentTypeTranslationRepository : ILegalDocumentTypeTranslationRepository
{
    private readonly DoorsDbContext _context;

    public LegalDocumentTypeTranslationRepository(DoorsDbContext context)
    {
        _context = context;
    }

    public async Task<LegalDocumentTypeTranslation?> GetByTypeAndLanguageAsync(int typeId, int languageId)
    {
        return await _context.LegalDocumentTypeTranslations
            .FirstOrDefaultAsync(t => t.DocumentTypeId == typeId && t.LanguageId == languageId);
    }
    public async Task<LegalDocumentTypeTranslation?> GetByTypeIdAndLanguageAsync(int documentTypeId, int languageId)
    {
        return await _context.LegalDocumentTypeTranslations
            .AsNoTracking()
            .FirstOrDefaultAsync(t =>
                t.DocumentTypeId == documentTypeId &&
                t.LanguageId == languageId);
    }
}
