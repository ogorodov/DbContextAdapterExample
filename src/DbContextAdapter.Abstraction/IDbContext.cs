namespace DbContextAdapter.Abstraction;

/// <summary>
/// Базовая абстракция контекста БД
/// </summary>
public interface IDbContext
{
    Task<IAsyncDisposable> OpenConnectionAsync(CancellationToken cancellationToken = default);
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    
    IQueryExecutor Executor { get; }
    IQueryBuilder Builder { get; }
}