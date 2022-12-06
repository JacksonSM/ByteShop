using ByteShop.Domain.Entities;
using ByteShop.Domain.Interfaces.Repositories;
using ByteShop.Infrastructure.Context;

namespace ByteShop.Infrastructure.Repositories;
public class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly ByteShopDbContext _context;

    public ProductRepository(ByteShopDbContext context): base(context)
    {
        _context = context;
    }

    public Task<List<Product>> GetAllAsync(IQueryable<Product> query)
    {
        throw new NotImplementedException();
    }
}
