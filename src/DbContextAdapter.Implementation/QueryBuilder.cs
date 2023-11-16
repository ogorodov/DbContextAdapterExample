using DbContextAdapter.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace DbContextAdapter.Implementation;

public sealed class QueryBuilder : IQueryBuilder
{
    public IQueryable<TEntity> AsNoTracking<TEntity>(IQueryable<TEntity> query) where TEntity : class =>
        query.AsNoTracking();
}