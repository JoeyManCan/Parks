using Microsoft.EntityFrameworkCore;
using Parks.API.Data;
using Parks.API.Repositories.Abstractions;

namespace Parks.API.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ParksDbContext Context;
        protected readonly DbSet<T> DbSet;
        public Repository(ParksDbContext parksDbContext)
        {
            Context = parksDbContext;
            DbSet = parksDbContext.Set<T>();
        }

        public async Task<int> CreateAsync(T entity)
        {
            await DbSet.AddAsync(entity);
            return Context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return DbSet.ToList<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int? id)
        {
            return await DbSet.FindAsync(id);
        }

        public int Remove(T entity)
        {
            DbSet.Remove(entity);
            return Context.SaveChanges();
        }

        public int Update(T entity)
        {
            DbSet.Update(entity);
            return Context.SaveChanges();
        }
    }
}
