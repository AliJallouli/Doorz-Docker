using Domain.Entities.References;
using Domain.Interfaces;
using Infrastructure.Ef.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Ef;

public class SpokenLanguageRepository:ISpokenLanguageRepository
{
    private readonly DoorsDbContext _context;

    public SpokenLanguageRepository(DoorsDbContext context)
    {
        _context = context;
    }

    public async Task<SpokenLanguage?> GetByCodeAsync(string code)
    {
        return await _context.SpokenLanguages
            .AsNoTracking()
            .FirstOrDefaultAsync(lang => lang.Code == code);
    }
    public async Task<List<SpokenLanguage>> GetAllAsync()
    {
        var languages = await _context.SpokenLanguages
            .ToListAsync();
        
        return languages;
    }
    public async Task<SpokenLanguage> GetByIdAsync(int languageId)
    {
        var language = await _context.SpokenLanguages.FindAsync(languageId);

        if (language == null)
        {
            // Fallback : langue par défaut (ex : "fr")
            language = await _context.SpokenLanguages
                           .FirstOrDefaultAsync(l => l.Code == "en") 
                       ?? throw new Exception("Langue par défaut introuvable.");
        }

        return language;
    }


}