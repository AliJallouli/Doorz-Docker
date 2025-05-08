using Domain.Entities;

namespace Domain.Interfaces;

public interface IRoleRepository
{
    Task AddAsync(Role role);
    Task<bool> ExistsAsync(int roleId, int entityTypeId);
    Task<Role?> GetByIdAsync(int roleId);
    Task<Role?> GetByNameAndEntityTypeAsync(string name, int entityTypeId);
    Task<List<Role>> GetRolesByEntityTypeIdAsync(int entityTypeId);
    Task<List<Role>> GetRolesByEntityTypeNameAsync(string entityTypeName, string language);
    Task<int?> GetRoleIdByRoleNameAndEntityTypeName(string roleName, string entityTypeName);
}