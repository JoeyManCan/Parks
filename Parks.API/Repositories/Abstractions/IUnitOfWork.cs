using Microsoft.EntityFrameworkCore;

namespace Parks.API.Repositories.Abstractions
{
    public interface IUnitOfWork<TContext> where TContext : DbContext
    {
        INationalParkRepository NationalParkRepository { get; }
        ITrailRepository TrailRepository { get; }
    }
}
