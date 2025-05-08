using Domain.Entities.References;
using Domain.Interfaces;
using Infrastructure.Ef.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Ef;

public class InstitutionTypeRepository : IInstitutionTypeRepository
{
    private readonly DoorsDbContext _context;

    public InstitutionTypeRepository(DoorsDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsByIdAsync(int institutionTypeId)
    {
        return await _context.InstitutionTypes
            .AnyAsync(i => i.InstitutionTypeId == institutionTypeId);
    }

    public async Task<IEnumerable<InstitutionType>> GetAllAsync(string language)
    {
        var institutionTypes = await _context.Set<InstitutionType>()
            .Include(it => it.Translations).ThenInclude(t => t.Language)
            .ToListAsync();

        foreach (var institutionType in institutionTypes)
        {
            var translation = institutionType.Translations
                                  .FirstOrDefault(t => t.Language.Code == language)
                              ?? institutionType.Translations.FirstOrDefault(t => t.Language.Code == "en");

            if (translation != null)
            {
                institutionType.Name = translation.TranslatedName;
                institutionType.Description = translation.TranslatedDescription;
            }
        }

        return institutionTypes;
    }
}