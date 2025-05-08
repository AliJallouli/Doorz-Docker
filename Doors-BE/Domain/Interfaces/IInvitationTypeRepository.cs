namespace Domain.Interfaces;

public interface IInvitationTypeRepository
{
    Task<int> GetIdByNameAsync(string name);
}