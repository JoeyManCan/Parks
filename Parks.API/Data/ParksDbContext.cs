using Microsoft.EntityFrameworkCore;
using Parks.API.Models;

namespace Parks.API.Data
{
    public class ParksDbContext :DbContext
    {
        public ParksDbContext(DbContextOptions<ParksDbContext> options):base(options)
        {

        }

        //although not referenced, these DBSets are used at runtime
        public virtual DbSet<NationalPark> NationalParks { get; set; } = null!;
        public virtual DbSet<Trail> Trails { get; set; } = null!;

    }
}
