using CleanArchitecture.Application.Common.Interfaces;
using DbContextAdapter.Abstraction;

namespace CleanArchitecture.Application.Common.Models;

public class PaginatedList<T>
{
    public IReadOnlyCollection<T> Items { get; }
    public int PageNumber { get; }
    public int TotalPages { get; }
    public int TotalCount { get; }

    public PaginatedList(IReadOnlyCollection<T> items, int count, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
        Items = items;
    }

    public bool HasPreviousPage => PageNumber > 1;

    public bool HasNextPage => PageNumber < TotalPages;

    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> query, int pageNumber, int pageSize, 
        IApplicationDbContext dbContext, CancellationToken cancellationToken = default)
    {
        await using var _ = await dbContext.OpenConnectionAsync(cancellationToken);

        var count = await query.CountAsync(dbContext.Executor, cancellationToken);
        var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(dbContext.Executor, cancellationToken);

        return new PaginatedList<T>(items, count, pageNumber, pageSize);
    }
}
