using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace Infrastructure.Ef.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task SaveChangesAsync();
    Task CommitAsync();
    Task RollbackAsync();
}