namespace Domain.Interfaces.Services;

public interface ISuperRoleRepository
{
    Task<int> GetSuperRoleIdAsync(string roleName);
}