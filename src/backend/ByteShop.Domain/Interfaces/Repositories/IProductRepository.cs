using ByteShop.Domain.Entities;

namespace ByteShop.Domain.Interfaces.Repositories;
public interface IProductRepository
{
    Task AddAsync(Product product);
}
