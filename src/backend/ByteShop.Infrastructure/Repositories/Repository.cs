using ByteShop.Domain.Entities;
using ByteShop.Domain.Interfaces.Repositories;
using ByteShop.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ByteShop.Infrastructure.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : Entity, new()
    {
        protected readonly ByteShopDbContext _dbContext;
        protected readonly DbSet<T> DbSet;

        public Repository(ByteShopDbContext dbContext)
        {
            _dbContext = dbContext;
            DbSet = dbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate)=>
            await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        

        public virtual async Task<T> GetByIdAsync(int id)=>
            await DbSet.FindAsync(id);
        

        public virtual async Task<List<T>> GetAllAsync()=>
             await DbSet.ToListAsync();
        

        public virtual async Task AddAsync(T entity)
        {
            await DbSet.AddAsync(entity);
        }

        public virtual void Update(T entity)
        {
            DbSet.Update(entity);
        }

        public virtual void Remove(int id)
        {
            DbSet.Remove(new T { Id = id });
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
