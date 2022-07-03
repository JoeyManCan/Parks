using Microsoft.EntityFrameworkCore;
using Parks.API.Models;

namespace Parks.API.Data
{
    public class ParksDbContext :DbContext
    {
        public ParksDbContext(DbContextOptions<ParksDbContext> options):base(options)
        {

        }

        public DbSet<NationalPark> NationalParks { get; set; } = null!;
    }
}
