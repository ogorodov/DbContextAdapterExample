using CleanArchitecture.Domain.Entities;
using DbContextAdapter.Abstraction;

namespace CleanArchitecture.Application.Common.Interfaces;

public interface IApplicationDbContext : IDbContext
{
    IRepository<TodoList> TodoLists { get; }

    IRepository<TodoItem> TodoItems { get; }
}
