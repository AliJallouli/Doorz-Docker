using Domain.Entities.Legals;
using Domain.Interfaces.Legals;
using Infrastructure.Ef.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Ef.Legals;

public class LegalDocumentClauseTranslationRepository: ILegalDocumentClauseTranslationRepository
{
    private readonly DoorsDbContext _context;

    public LegalDocumentClauseTranslationRepository(DoorsDbContext context)
    {
        _context = context;
    }

    public async Task<List<LegalDocumentClauseTranslation>> GetByDocumentIdAndLanguageAsync(int documentId, int languageId)
    {
        return await _context.LegalDocumentClauseTranslations
            .Include(t => t.Clause)
            .Where(t => t.Clause.DocumentId == documentId && t.LanguageId == languageId)
            .OrderBy(t => t.Clause.OrderIndex)
            .ToListAsync();
    }
}