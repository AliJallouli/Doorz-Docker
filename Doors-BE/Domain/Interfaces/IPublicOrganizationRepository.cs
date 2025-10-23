using Domain.Entities;

namespace Domain.Interfaces;

public interface IPublicOrganizationRepository
{
    Task<PublicOrganization> AddAsync(PublicOrganization publicOrganization);
    Task<PublicOrganization?> GetByIdAsync(int publicOrganizationId);
    Task<bool> ExistsAsync(int publicOrganizationId);
    Task<bool> ExistNameAsync(string name);
}