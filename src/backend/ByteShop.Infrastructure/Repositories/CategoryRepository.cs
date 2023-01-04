using ByteShop.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using ByteShop.Infrastructure.Context;
using ByteShop.Domain.Entities;

namespace ByteShop.Infrastructure.Repositories;
public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private readonly ByteShopDbContext _context;

    public CategoryRepository(ByteShopDbContext context): base(context)
    {
        _context = context;
    }

    public async Task<bool> ExistsById(int id)=>
        await _context.Category.AnyAsync(x =>x.Id == id);

    public async Task<Category> GetByIdWithAssociationAsync(int id)=>
         await _context.Category
            .AsNoTracking()
            .Include(x => x.ParentCategory)
            .Include(x => x.ChildCategories)
            .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<Category> GetByIdWithChildCategoriesAndProductsAsync(int id) =>
        await _context.Category
            .AsNoTracking()
            .Include(x => x.Products)
            .Include(x => x.ChildCategories)
            .FirstOrDefaultAsync(x => x.Id == id);

}
