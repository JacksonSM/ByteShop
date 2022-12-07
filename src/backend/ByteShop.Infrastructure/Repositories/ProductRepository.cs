using ByteShop.Domain.Entities;
using ByteShop.Domain.Interfaces.Repositories;
using ByteShop.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

    public async Task<int> GetCountAsync(IQueryable<Product> query)
    {
        throw new NotImplementedException();
    }
        

    public IQueryable<Product> GetQueryable() =>
         _context.Product.AsQueryable();

    public override async Task<Product> GetByIdAsync(int id)=>
        await _context.Product.FirstOrDefaultAsync(x => x.IsActive == true && x.Id == id);
    
}
