namespace Application.Contracts.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T?> GetAsync(int id);
        Task<IEnumerable<T>> GetByNameAsync(string name);

        Task<T> AddAsync(T item);

        Task<T?> UpdateAsync(int id, T item);

        Task<bool> DeleteAsync(int id);
    }
}
