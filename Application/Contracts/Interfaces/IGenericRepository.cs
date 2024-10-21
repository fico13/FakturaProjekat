namespace Application.Contracts.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetBySifraAsync(string sifra);

        Task<T> AddAsync(T item);

        Task<T?> UpdateAsync(T item);

        Task<bool> DeleteAsync(string sifra);
    }
}
