using Domain.Entities.References;

namespace Domain.Interfaces;

public interface IInstitutionTypeRepository
{
    Task<bool> ExistsByIdAsync(int institutionTypeId);
    Task<IEnumerable<InstitutionType>> GetAllAsync(string language);
}