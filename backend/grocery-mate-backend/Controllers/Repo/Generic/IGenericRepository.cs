using System.Linq.Expressions;
using grocery_mate_backend.Data.DataModels.UserManagement;
using Microsoft.AspNetCore.Identity;

namespace grocery_mate_backend.Controllers.Repo;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> All();
    Task<T> GetById(Guid id);
    Task<bool> Add(T entity);
    Task<bool> Delete(Guid id);
    Task<bool> Upsert(T entity);
    Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
}