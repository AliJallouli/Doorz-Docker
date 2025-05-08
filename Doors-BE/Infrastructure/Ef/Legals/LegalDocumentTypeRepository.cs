using Domain.Entities.Legals;
using Domain.Interfaces.Legals;
using Infrastructure.Ef.Data;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Ef.Legals;

public class LegalDocumentTypeRepository : ILegalDocumentTypeRepository
{
    private readonly DoorsDbContext _context;

    public LegalDocumentTypeRepository(DoorsDbContext context)
    {
        _context = context;
    }

    public async Task<LegalDocumentType?> GetByNameAsync(string name)
    {
        return await _context.LegalDocumentTypes
            .FirstOrDefaultAsync(d => d.Name == name);
    }

    public async Task<List<LegalDocumentType>> GetAllAsync()
    {
        return await _context.LegalDocumentTypes.ToListAsync();
    }

    public async Task AddAsync(LegalDocumentType entity)
    {
        await _context.LegalDocumentTypes.AddAsync(entity);
    }
    public async Task<List<LegalDocumentType>> GetAllWithTranslationAsync(string languageCode)
    {
        var types = await _context.LegalDocumentTypes
            .Include(t => t.Translations)
                .ThenInclude(tr => tr.Language)
            .ToListAsync();

        foreach (var type in types)
            type.Translations = type.Translations
                .Where(tr => tr.Language.Code == languageCode)
                .ToList();

        return types;
    }


}
