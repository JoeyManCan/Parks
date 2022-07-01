namespace Parks.API.Repositories.Abstractions
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int? id);
        Task<int> CreateAsync(T entity);

        /// <summary>
        /// Due to DB concurrency, there's no UpdateAsync or Delete Async
        /// </summary>
        /// <param name="author"></param>
        /// <returns>number of lines changed</returns>
        int Update(T entity);

        /// <summary>
        /// Due to DB concurrency, there's no UpdateAsync or Delete Async
        /// </summary>
        /// <param name="author"></param>
        /// <returns>number of lines changed</returns>
        int Remove(T entity);


    }
}
