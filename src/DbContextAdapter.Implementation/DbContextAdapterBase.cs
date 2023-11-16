using DbContextAdapter.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DbContextAdapter.Implementation;

public abstract class DbContextAdapterBase<TDbContext> : IDbContext
    where TDbContext : DbContext
{
    protected readonly TDbContext _dbContext;
    private readonly ConnectionDispatcher _connectionDispatcher;

    protected DbContextAdapterBase(TDbContext dbContext, IQueryExecutor queryExecutor, IQueryBuilder queryBuilder)
    {
        _dbContext = dbContext;
        _connectionDispatcher = new ConnectionDispatcher(_dbContext.Database);
        Executor = queryExecutor;
        Builder = queryBuilder;
    }

    public Task<IAsyncDisposable> OpenConnectionAsync(CancellationToken cancellationToken = default) =>
        _connectionDispatcher.OpenConnectionAsync(cancellationToken);

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        return new DbContextTransactionWrapper(transaction);
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _dbContext.SaveChangesAsync(cancellationToken);

    public IQueryExecutor Executor { get; }
    public IQueryBuilder Builder { get; }

    #region ConnectionDispatcher

    private sealed class ConnectionDispatcher : IAsyncDisposable
    {
        private readonly DatabaseFacade _databaseFacade;

        private bool _isOpened;
        
        public ConnectionDispatcher(DatabaseFacade databaseFacade)
        {
            _databaseFacade = databaseFacade;
        }

        internal async Task<IAsyncDisposable> OpenConnectionAsync(CancellationToken cancellationToken)
        {
            if (_isOpened)
                throw new InvalidOperationException("Соединение уже открыто");
            
            await _databaseFacade.OpenConnectionAsync(cancellationToken);
            _isOpened = true;

            return this;
        }

        public async  ValueTask DisposeAsync()
        {
            if(_isOpened == false)
                throw new InvalidOperationException("Соединение уже закрыто");

            await _databaseFacade.CloseConnectionAsync();
            _isOpened = false;
        }
    }
    
    #endregion
    
    #region IDbContextTransaction imnplementation

    private sealed class DbContextTransactionWrapper : IDbContextTransaction
    {
        private readonly Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction _sourceTransaction;

        public DbContextTransactionWrapper(Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction sourceTransaction)
        {
            _sourceTransaction = sourceTransaction;
        }

        public void Dispose() => _sourceTransaction.Dispose();
        public ValueTask DisposeAsync() => _sourceTransaction.DisposeAsync();
        public Task CommitAsync(CancellationToken cancellationToken = default) => _sourceTransaction.CommitAsync(cancellationToken);
        public Task RollbackAsync(CancellationToken cancellationToken = default) => _sourceTransaction.RollbackAsync(cancellationToken);
    }

    #endregion
}