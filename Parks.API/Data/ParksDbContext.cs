using Microsoft.EntityFrameworkCore;
using Parks.API.Models;

namespace Parks.API.Data
{
    public class ParksDbContext :DbContext
    {
        public ParksDbContext(DbContextOptions<ParksDbContext> options):base(options)
        {

        }

    }
}
