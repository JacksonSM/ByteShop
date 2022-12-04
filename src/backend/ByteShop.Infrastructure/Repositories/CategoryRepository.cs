using ByteShop.Domain.Interfaces.Repositories;
using ByteShop.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ByteShop.Infrastructure.Repositories;
public class CategoryRepository : ICategoryRepository
{
    private readonly ByteShopDbContext _context;

    public CategoryRepository(ByteShopDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsById(int id)=>
        await _context.Category.AnyAsync(x =>x.Id == id);
}
