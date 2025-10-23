namespace Application.UseCases.Auth.Service;

public interface IUserActionLogService
{
    Task<bool> CanPerformActionAsync(int userId, string actionType);
    Task LogActionAsync(int userId, string actionType, int userAgentId, string ipAddress,string oldValue, string newValue);
}