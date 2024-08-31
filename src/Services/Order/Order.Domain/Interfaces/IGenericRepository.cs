using Order.Domain.Common;
using System.Linq.Expressions;

namespace Order.Domain.Interfaces;

public interface IGenericRepository<T> where T : MongoBaseModel, new()
{
    Task<T> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
