using Infrastructure.Ef.Data;
using Infrastructure.Ef.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Ef;

public class UnitOfWork : IUnitOfWork
{
    private readonly DoorsDbContext _context;

    public UnitOfWork(DoorsDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _context.Database.BeginTransactionAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
    
}