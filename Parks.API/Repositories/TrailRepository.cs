using Microsoft.EntityFrameworkCore;
using Parks.API.Data;
using Parks.API.Models;
using Parks.API.Repositories.Abstractions;

namespace Parks.API.Repositories
{
    public class TrailRepository : Repository<Trail>, ITrailRepository
    {
        public TrailRepository(ParksDbContext parksDbContext) : base(parksDbContext)
        {
        }
        public async Task<bool> IsTrailThere(string name)
        {
            var trail = await DbSet.FirstOrDefaultAsync(trail => trail.Name == name);
            return trail != null;
        }

        public IEnumerable<Trail> GetTrails()
        {
            return DbSet.Include(trail => trail.NationalPark).OrderBy(trl =>trl.Name).ToList();
        }

        public Trail? GetTrailById(int id)
        {
            return DbSet.Include(trl => trl.NationalPark)
                .FirstOrDefault(trail => id == trail.Id);
        }

        public IList<Trail> GetTrailsInPark(int parkId)
        {
            return DbSet.Include(trail => trail.NationalPark)
                .Where(park => park.NationalParkId == parkId).ToList();
        }

    }
}
