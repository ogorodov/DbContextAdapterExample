using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using DbContextAdapter.Abstraction;
using DbContextAdapter.Implementation;

namespace CleanArchitecture.Infrastructure.Persistence;

public sealed class ApplicationDbContextAdapter : DbContextAdapterBase<ApplicationDbContext>, IApplicationDbContext
{
    private readonly Lazy<IRepository<TodoList>> _todoLists;
    private readonly Lazy<IRepository<TodoItem>> _todoItems;
    
    public ApplicationDbContextAdapter(ApplicationDbContext dbContext, IQueryExecutor queryExecutor, IQueryBuilder queryBuilder) :
        base(dbContext, queryExecutor, queryBuilder)
    {
        _todoLists = new Lazy<IRepository<TodoList>>(() => new GenericRepository<TodoList>(dbContext.Set<TodoList>()));
        _todoItems = new Lazy<IRepository<TodoItem>>(() => new GenericRepository<TodoItem>(dbContext.Set<TodoItem>()));
    }

    public IRepository<TodoList> TodoLists => _todoLists.Value;
    public IRepository<TodoItem> TodoItems => _todoItems.Value;
}