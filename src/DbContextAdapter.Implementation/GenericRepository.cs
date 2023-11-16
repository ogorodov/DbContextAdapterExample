using System.Collections;
using System.Linq.Expressions;
using DbContextAdapter.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace DbContextAdapter.Implementation;

public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly DbSet<TEntity> _dbSet;

    public GenericRepository(DbSet<TEntity> dbSet)
    {
        _dbSet = dbSet;
    }

    public void Add(TEntity entity) => _dbSet.Add(entity);
    public void Remove(TEntity entity) => _dbSet.Remove(entity);
    public void RemoveRange(IEnumerable<TEntity> entities) => _dbSet.RemoveRange(entities);

    public ValueTask<TEntity?> FindAsync(params object?[]? keyValues) => _dbSet.FindAsync(keyValues);
    public ValueTask<TEntity?> FindAsync(object?[]? keyValues, CancellationToken cancellationToken) => _dbSet.FindAsync(keyValues, cancellationToken);

    public IAsyncEnumerable<TEntity> AsAsyncEnumerable() => _dbSet.AsAsyncEnumerable();

    public Task<int> ExecuteDeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) =>
        _dbSet.Where(predicate).ExecuteDeleteAsync(cancellationToken);
    
    #region IEnumerable Implementation

    public IEnumerator<TEntity> GetEnumerator() => ((IQueryable<TEntity>)_dbSet).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IQueryable<TEntity>)_dbSet).GetEnumerator();
    
    #endregion

    #region IQueryable Implementation

    public Type ElementType => ((IQueryable<TEntity>)_dbSet).ElementType;
    public Expression Expression => ((IQueryable<TEntity>)_dbSet).Expression;
    public IQueryProvider Provider => ((IQueryable<TEntity>)_dbSet).Provider;
    
    #endregion

    
}