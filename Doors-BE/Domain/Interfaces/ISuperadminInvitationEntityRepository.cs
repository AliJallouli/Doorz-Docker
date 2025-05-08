using Domain.Entities;

namespace Domain.Interfaces;

public interface ISuperadminInvitationEntityRepository
{
    Task AddAsync(SuperadminInvitationEntity entity);
    Task<SuperadminInvitationEntity> GetByInvitationIdAsync(int invitationId);
    Task DeleteAsync(int id);
}