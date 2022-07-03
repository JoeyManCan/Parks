using Microsoft.EntityFrameworkCore;
using Parks.API.Data;
using Parks.API.Models;
using Parks.API.Repositories.Abstractions;

namespace Parks.API.Repositories
{
    public class NationalParkRepository : Repository<NationalPark>, INationalParkRepository
    {
        public NationalParkRepository(ParksDbContext parksDbContext) : base(parksDbContext)
        {
        }

        public async Task<bool> IsParkThere(string parkName)
        {
            var nationalPark = await DbSet.FirstOrDefaultAsync(park => park.Name == parkName);
            return nationalPark != null;
        }

    }
}
