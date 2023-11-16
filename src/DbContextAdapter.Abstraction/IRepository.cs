using System.Linq.Expressions;

namespace DbContextAdapter.Abstraction;

public interface IRepository<TEntity> : IQueryable<TEntity> where TEntity : class
{
    void Add(TEntity entity);
    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);

    ValueTask<TEntity?> FindAsync(params object?[]? keyValues);
    ValueTask<TEntity?> FindAsync(object?[]? keyValues, CancellationToken cancellationToken);
    
    IAsyncEnumerable<TEntity> AsAsyncEnumerable();

    Task<int> ExecuteDeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
}