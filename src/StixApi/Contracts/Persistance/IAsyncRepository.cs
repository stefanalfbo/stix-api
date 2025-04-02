namespace StixApi.Contracts.Persistance
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T?> GetByIdAsync(string id);
        Task DeleteAsync(T entity);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
    }
}