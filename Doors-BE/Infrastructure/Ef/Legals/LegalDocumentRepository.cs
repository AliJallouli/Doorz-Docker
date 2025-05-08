using Domain.Entities.Legals;
using Domain.Interfaces.Legals;
using Infrastructure.Ef.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Ef.Legals;

public class LegalDocumentRepository : ILegalDocumentRepository
{
    private readonly DoorsDbContext _context;

    public LegalDocumentRepository(DoorsDbContext context)
    {
        _context = context;
    }

    public async Task<LegalDocument?> GetActiveByTypeAsync(string typeName)
    {
        return await _context.LegalDocuments
            .Include(d => d.DocumentType)
            .Where(d => d.IsActive && d.DocumentType.Name == typeName)
            .OrderByDescending(d => d.PublishedAt)
            .FirstOrDefaultAsync();
    }

    public async Task<LegalDocument?> GetByIdAsync(int id)
    {
        return await _context.LegalDocuments
            .Include(d => d.DocumentType)
            .FirstOrDefaultAsync(d => d.DocumentId == id);
    }

    public async Task<List<LegalDocument>> GetAllActiveAsync()
    {
        return await _context.LegalDocuments
            .Where(d => d.IsActive)
            .ToListAsync();
    }

    public async Task AddAsync(LegalDocument document)
    {
        await _context.LegalDocuments.AddAsync(document);
    }
    public async Task<LegalDocument?> GetActiveByTypeAndLanguageAsync(string typeName, string languageCode)
    {
        return await _context.LegalDocuments
            .Include(d => d.DocumentType)
            .Include(d => d.Clauses.OrderBy(c => c.OrderIndex))
                .ThenInclude(c => c.Translations.Where(t => t.Language.Code == languageCode))
            .Where(d => d.IsActive && d.DocumentType.Name == typeName)
            .FirstOrDefaultAsync();
    }

}
