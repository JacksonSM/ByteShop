using ByteShop.Domain.Entities;

namespace ByteShop.Domain.Interfaces.Repositories;
public interface IProductRepository : IRepository<Product>
{
    Task<List<Product>> GetAllAsync(IQueryable<Product> query);
    IQueryable<Product> GetQueryable();
    Task<int> GetCountAsync(IQueryable<Product> query);
}
