namespace StixApi.Contracts.Persistance
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> ListAllAsync();
    }
}