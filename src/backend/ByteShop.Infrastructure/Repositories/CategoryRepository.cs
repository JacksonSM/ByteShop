﻿using ByteShop.Domain.Entities;
using ByteShop.Domain.Interfaces.Repositories;
using ByteShop.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Category> GetByIdWithAssociation(int id)=>
         await _context.Category
            .AsNoTracking()
            .Include(x => x.ParentCategory)
            .Include(x => x.ParentCategory.ParentCategory)
            .Include(x => x.ChildCategories)
            .FirstOrDefaultAsync(x => x.Id == id);

    
}
