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
    }
}
