namespace HotelListing.Domain.Interfaces.IRepositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(int id);
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> GetAllAsQueryable();
        Task CreateAsync(T element);
        Task UpdateAsync(T element);
        Task DeleteAsync(int id);
        Task<bool> Exists(int id);
    }

}



