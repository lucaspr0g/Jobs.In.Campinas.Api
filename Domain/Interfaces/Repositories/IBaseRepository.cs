namespace Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<T> CreateAsync(T obj);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(string id);
        Task RemoveAsync(string id);
        Task UpdateAsync(string id, T obj);
    }
}