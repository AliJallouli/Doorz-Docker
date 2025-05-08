using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Ef.Interfaces;

public interface IUnitOfWork
{
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task SaveChangesAsync();
}