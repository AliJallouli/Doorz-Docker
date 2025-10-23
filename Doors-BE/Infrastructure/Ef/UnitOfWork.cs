using Infrastructure.Ef.Data;
using Infrastructure.Ef.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Ef;

public class UnitOfWork : IUnitOfWork
{
    private readonly DoorsDbContext _context;
    private readonly ILogger<UnitOfWork> _logger;

    public UnitOfWork(DoorsDbContext context, ILogger<UnitOfWork> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        if (_context.Database.CurrentTransaction != null)
        {
            _logger.LogWarning("Une transaction est déjà en cours, retour de la transaction existante.");
            return _context.Database.CurrentTransaction;
        }

        _logger.LogInformation("Début de BeginTransactionAsync, état de la connexion : {ConnectionState}", 
            _context.Database.GetDbConnection().State);
        var transaction = await _context.Database.BeginTransactionAsync();
        _logger.LogInformation("Transaction démarrée avec succès");
        return transaction;
    }

    public async Task SaveChangesAsync()
    {
        _logger.LogInformation("Enregistrement des changements dans la base de données");
        await _context.SaveChangesAsync();
    }

    public async Task CommitAsync()
    {
        if (_context.Database.CurrentTransaction != null)
        {
            _logger.LogInformation("Validation de la transaction");
            await _context.Database.CurrentTransaction.CommitAsync();
        }
    }

    public async Task RollbackAsync()
    {
        if (_context.Database.CurrentTransaction != null)
        {
            _logger.LogInformation("Annulation de la transaction");
            await _context.Database.CurrentTransaction.RollbackAsync();
        }
    }

    public void Dispose()
    {
        _logger.LogInformation("Disposal du UnitOfWork et du DbContext");
        _context.Dispose();
    }
}