using Microsoft.EntityFrameworkCore;
using Parks.API.Data;
using Parks.API.Repositories.Abstractions;

namespace Parks.API.Repositories
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        public INationalParkRepository NationalParkRepository { get; private set; } = null!;
        public ITrailRepository TrailRepository { get; private set; } = null!;

        private readonly ParksDbContext _parksDbContext;

        public UnitOfWork(ParksDbContext parksDbContext)
        {
            _parksDbContext = parksDbContext;

            NationalParkRepository = new NationalParkRepository(_parksDbContext);

            TrailRepository = new TrailRepository(_parksDbContext);
        }
    }
}
