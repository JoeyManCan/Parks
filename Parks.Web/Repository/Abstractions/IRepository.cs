using Parks.Web.Static;

namespace Parks.Web.Repository.Abstractions
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>?> GetAllAsync(string url);
        Task<T?> GetByIdAsync(string url, int id);
        Task<ResponseCodes> CreateAsync(string url, T entity);
        Task<ResponseCodes> UpdateAsync(string url, T entity);

        Task<ResponseCodes> DeleteAsync(string url, int id);
    }
}
