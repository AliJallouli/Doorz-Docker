using Domain.Entities;

namespace Domain.Interfaces;

public interface ISuperadminInvitationRepository
{
    Task AddAsync(SuperadminInvitation invitation);
    Task<SuperadminInvitation> GetByIdAsync(int id);
    Task<SuperadminInvitation?> GetByTokenAsync(string token);
    Task<bool> ExistsByEmailAsync(string email);
    Task UpdateAsync(SuperadminInvitation invitation);
    Task DeleteAsync(int id);
}