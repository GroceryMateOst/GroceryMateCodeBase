using System.Linq.Expressions;
using grocery_mate_backend.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace grocery_mate_backend.Controllers.Repo.Generic;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private GroceryContext _context;
    protected readonly DbSet<T> _dbSet;

    protected GenericRepository(GroceryContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public virtual async Task<IEnumerable<T>> All()
    {
        throw new NotImplementedException();
    }

    public virtual async Task<T> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public virtual async Task<bool> Add(T entity)
    {
        await _context.AddAsync(entity);
        return true;
    }

    public virtual async Task<bool> Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public virtual async Task<bool> Upsert(T entity)
    {
        throw new NotImplementedException();
    }

    public virtual async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
    {
        throw new NotImplementedException();
    }
}

public class InstanceNotFoundException : System.Exception
{
    public InstanceNotFoundException() : base("The searched instance could not be found in the database!")
    {
    }

    public InstanceNotFoundException(string message) : base(message)
    {
    }
}