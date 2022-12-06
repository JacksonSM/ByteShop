using ByteShop.Domain.Entities;
using System.Linq.Expressions;

namespace ByteShop.Domain.Interfaces.Repositories;
public interface IRepository<T> : IDisposable where T : Entity
{
    Task AddAsync(T entity);
    Task<T> GetByIdAsync(int id);
    Task<List<T>> GetAllAsync();
    void Update(T Obj);
    void Remove(int id);
    Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate);
}