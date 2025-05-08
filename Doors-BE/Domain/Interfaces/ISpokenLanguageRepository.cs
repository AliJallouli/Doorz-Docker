using Domain.Entities.References;

namespace Domain.Interfaces;

public interface ISpokenLanguageRepository
{
    Task<SpokenLanguage?> GetByCodeAsync(string code);
    Task<List<SpokenLanguage>> GetAllAsync();
    Task<SpokenLanguage> GetByIdAsync(int languageId);
}

