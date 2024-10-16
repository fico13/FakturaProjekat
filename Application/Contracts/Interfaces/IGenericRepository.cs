namespace Application.Contracts.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetBySifraAsync(string name);

        Task<T> AddAsync(T item);

        Task<T?> UpdateAsync(string sifra, T item);

        Task<bool> DeleteAsync(string sifra);
    }
}
