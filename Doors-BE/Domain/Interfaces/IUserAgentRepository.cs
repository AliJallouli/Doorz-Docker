using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserAgentRepository
{
    Task<UserAgent?> GetAsync(string userAgentValue);
    Task<int> AddAsync(UserAgent userAgent);
}