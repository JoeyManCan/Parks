using Microsoft.EntityFrameworkCore;
using Parks.API.Models;

namespace Parks.API.Data
{
    public class ParksDbContext :DbContext
    {
        public ParksDbContext(DbContextOptions<ParksDbContext> options):base(options)
        {

        }

        //used for migrations
        public virtual DbSet<NationalPark> NationalParks { get; set; } = null!;
        public virtual DbSet<Trail> Trails { get; set; } = null!;

    }
}
