using ByteShop.Domain.Entities;
using ByteShop.Domain.Interfaces.Repositories;
using ByteShop.Infrastructure.Context;

namespace ByteShop.Infrastructure.Repositories;
public class ProductRepository : IProductRepository
{
    private readonly ByteShopDbContext _context;

    public ProductRepository(ByteShopDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Product product)=>
        await _context.Product.AddAsync(product); 
}
