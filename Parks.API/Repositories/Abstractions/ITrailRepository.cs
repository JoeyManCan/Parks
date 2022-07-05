using Parks.API.Models;

namespace Parks.API.Repositories.Abstractions
{
    public interface ITrailRepository : IRepository<Trail>
    {
        IList<Trail> GetTrailsInPark(int parkId);
        //IEnumerable<Trail> GetTrails();
        Trail? GetTrailById(int id);
        Task<bool> IsTrailThere(string name);
        IEnumerable<Trail> GetTrails();
    }
}
