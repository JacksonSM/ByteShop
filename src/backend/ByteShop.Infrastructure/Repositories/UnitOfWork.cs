using ByteShop.Domain.Interfaces.Repositories;
using ByteShop.Infrastructure.Context;

namespace ByteShop.Infrastructure.Repositories;
internal class UnitOfWork : IUnitOfWork
{
    private readonly ByteShopDbContext _context;

    public UnitOfWork(ByteShopDbContext context)
    {
        _context = context;
    }
    public void Commit()
    {
        _context.SaveChanges();
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}
