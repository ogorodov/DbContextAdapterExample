using System.Linq.Expressions;
using DbContextAdapter.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace DbContextAdapter.Implementation;

public sealed class QueryExecutor : IQueryExecutor
{
    public Task<TEntity> FirstAsync<TEntity>(IQueryable<TEntity> query, CancellationToken cancellationToken = default) =>
        query.FirstAsync(cancellationToken);

    public Task<TEntity?> FirstOrDefaultAsync<TEntity>(IQueryable<TEntity> query, CancellationToken cancellationToken = default) =>
        query.FirstOrDefaultAsync(cancellationToken);

    public Task<TEntity?> SingleOrDefaultAsync<TEntity>(IQueryable<TEntity> query, CancellationToken cancellationToken = default) =>
        query.SingleOrDefaultAsync(cancellationToken);

    public Task<List<TEntity>> ToListAsync<TEntity>(IQueryable<TEntity> query, CancellationToken cancellationToken = default) =>
        query.ToListAsync(cancellationToken);

    public Task<TEntity[]> ToArrayAsync<TEntity>(IQueryable<TEntity> query, CancellationToken cancellationToken = default) =>
        query.ToArrayAsync(cancellationToken);

    public Task<int> CountAsync<TEntity>(IQueryable<TEntity> query, CancellationToken cancellationToken = default) =>
        query.CountAsync(cancellationToken);

    public Task<bool> AnyAsync<TSource>(IQueryable<TSource> query, CancellationToken cancellationToken = default) =>
        query.AnyAsync(cancellationToken);

    public Task<bool> AnyAsync<TSource>(IQueryable<TSource> query, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default) =>
        query.AnyAsync(predicate, cancellationToken);

    public Task<bool> AllAsync<TSource>(IQueryable<TSource> query, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default) =>
        query.AllAsync(predicate, cancellationToken);
}