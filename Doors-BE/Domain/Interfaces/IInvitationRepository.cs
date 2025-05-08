namespace Domain.Interfaces;

public interface IInvitationRepository
{
    Task<T> AddAsync<T>(T invitation) where T : class;
    Task<T?> GetByCodeAsync<T>(string code) where T : class;
    Task UpdateAsync<T>(T invitation) where T : class;
}