using ByteShop.Domain.Entities;

namespace ByteShop.Domain.Interfaces.Repositories;
public interface ICategoryRepository : IRepository<Category>
{
    Task<bool> ExistsById(int id);
    Task<Category> GetByIdWithAssociationAsync(int id);
    Task<List<Category>> GetAllWithAssociationAsync();
    Task<Category> GetByIdWithProductsAsync(int id);
}
