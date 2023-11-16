using System.Linq.Expressions;

namespace DbContextAdapter.Abstraction;

public interface IQueryExecutor
{
    Task<TEntity> FirstAsync<TEntity>(IQueryable<TEntity> query, CancellationToken cancellationToken = default);
    Task<TEntity?> FirstOrDefaultAsync<TEntity>(IQueryable<TEntity> query, CancellationToken cancellationToken = default);
    Task<TEntity?> SingleOrDefaultAsync<TEntity>(IQueryable<TEntity> query, CancellationToken cancellationToken = default);
    Task<List<TEntity>> ToListAsync<TEntity>(IQueryable<TEntity> query, CancellationToken cancellationToken = default);
    Task<TEntity[]> ToArrayAsync<TEntity>(IQueryable<TEntity> query, CancellationToken cancellationToken = default);
    Task<int> CountAsync<TEntity>(IQueryable<TEntity> query, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync<TSource>(IQueryable<TSource> query,CancellationToken cancellationToken = default);
    Task<bool> AnyAsync<TSource>(IQueryable<TSource> query, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default);
    Task<bool> AllAsync<TSource>(IQueryable<TSource> query, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default);
}

public static partial class QueryableExtension
{
    public static Task<TEntity> FirstAsync<TEntity>(this IQueryable<TEntity> query, IQueryExecutor executor, CancellationToken cancellationToken = default)
        => executor.FirstAsync(query, cancellationToken);

    public static Task<TEntity?> FirstOrDefaultAsync<TEntity>(this IQueryable<TEntity> query, IQueryExecutor executor, CancellationToken cancellationToken = default)
        => executor.FirstOrDefaultAsync(query, cancellationToken);

    public static Task<TEntity?> SingleOrDefaultAsync<TEntity>(this IQueryable<TEntity> query, IQueryExecutor executor, CancellationToken cancellationToken = default)
        => executor.SingleOrDefaultAsync(query, cancellationToken);

    public static Task<List<TEntity>> ToListAsync<TEntity>(this IQueryable<TEntity> query, IQueryExecutor executor, CancellationToken cancellationToken = default)
        => executor.ToListAsync(query, cancellationToken);

    public static Task<TEntity[]> ToArrayAsync<TEntity>(this IQueryable<TEntity> query, IQueryExecutor executor, CancellationToken cancellationToken = default)
        => executor.ToArrayAsync(query, cancellationToken);

    public static Task<int> CountAsync<TEntity>(this IQueryable<TEntity> query, IQueryExecutor executor, CancellationToken cancellationToken = default)
        => executor.CountAsync(query, cancellationToken);

    public static Task<bool> AnyAsync<TSource>(this IQueryable<TSource> query, IQueryExecutor executor, CancellationToken cancellationToken = default)
        => executor.AnyAsync(query, cancellationToken);

    public static Task<bool> AnyAsync<TSource>(this IQueryable<TSource> query, Expression<Func<TSource, bool>> predicate, IQueryExecutor executor,
        CancellationToken cancellationToken = default)
        => executor.AnyAsync(query, predicate, cancellationToken);

    public static Task<bool> AllAsync<TSource>(this IQueryable<TSource> query, Expression<Func<TSource, bool>> predicate, IQueryExecutor executor,
        CancellationToken cancellationToken = default)
        => executor.AllAsync(query, predicate, cancellationToken);
}