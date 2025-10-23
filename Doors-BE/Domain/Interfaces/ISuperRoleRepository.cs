namespace Domain.Interfaces;

public interface ISuperRoleRepository
{
    Task<int> GetSuperRoleIdAsync(string roleName);
}