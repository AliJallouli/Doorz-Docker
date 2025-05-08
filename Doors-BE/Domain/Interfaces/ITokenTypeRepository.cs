using Domain.Entities;

namespace Domain.Interfaces;

public interface ITokenTypeRepository
{
    Task<TokenType?> GetByNameAsync(string name);
    Task<IEnumerable<TokenType>> GetAllAsync();
    Task AddAsync(TokenType tokenType);
    Task UpdateAsync(TokenType tokenType);
    Task DeleteAsync(TokenType tokenType);
}