using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using DbContextAdapter.Abstraction;

namespace CleanArchitecture.Application.Common.Extensions;

public static class QueryableExtensions
{
    public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> query, int pageNumber, int pageSize, IApplicationDbContext dbContext,
        CancellationToken cancellationToken = default)
        where T : class
    {
        await using var _ = await dbContext.OpenConnectionAsync(cancellationToken);

        var count = await query.CountAsync(dbContext.Executor, cancellationToken);
        var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(dbContext.Executor, cancellationToken);

        return new PaginatedList<T>(items, count, pageNumber, pageSize);
    }
}