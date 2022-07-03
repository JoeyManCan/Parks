using Parks.API.Models;

namespace Parks.API.Repositories.Abstractions
{
    public interface INationalParkRepository : IRepository<NationalPark>
    {
        Task<bool> IsParkThere(string name);
    }
}
