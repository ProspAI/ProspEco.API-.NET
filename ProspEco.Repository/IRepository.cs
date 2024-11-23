namespace ProspEco.Repository
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T?> GetByIdAsync(long id);

        Task AddAsync(T entity);

        Task UpdateAsync(long id, T entity);

        Task DeleteAsync(long id);
    }
}