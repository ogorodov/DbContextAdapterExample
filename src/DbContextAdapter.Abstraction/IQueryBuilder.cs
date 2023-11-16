namespace DbContextAdapter.Abstraction;

public interface IQueryBuilder
{
    IQueryable<TEntity> AsNoTracking<TEntity>(IQueryable<TEntity> query) where TEntity : class;
}

public static partial class QueryableExtension
{
    public static IQueryable<TEntity> AsNoTracking<TEntity>(this IQueryable<TEntity> query, IQueryBuilder builder) where TEntity : class
        => builder.AsNoTracking(query);
}