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

        public Task<int> CreateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public T Get()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T?> GetByIdAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public int Remove(T entity)
        {
            throw new NotImplementedException();
        }

        public int Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
