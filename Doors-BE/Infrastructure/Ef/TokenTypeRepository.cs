using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Ef.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Ef;

public class TokenTypeRepository : ITokenTypeRepository
{
    private readonly DoorsDbContext _context;

    public TokenTypeRepository(DoorsDbContext context)
    {
        _context = context;
    }

    public async Task<TokenType?> GetByNameAsync(string name)
    {
        return await _context.TokenTypes.FirstOrDefaultAsync(t => t.Name == name);
    }

    public async Task<IEnumerable<TokenType>> GetAllAsync()
    {
        return await _context.TokenTypes
            .OrderBy(t => t.TokenTypeId)
            .ToListAsync();
    }

    public async Task AddAsync(TokenType tokenType)
    {
        _context.TokenTypes.Add(tokenType);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TokenType tokenType)
    {
        _context.TokenTypes.Update(tokenType);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TokenType tokenType)
    {
        _context.TokenTypes.Remove(tokenType);
        await _context.SaveChangesAsync();
    }
}