using HotelListing.Domain.Models;

namespace HotelListing.Domain.Interfaces.IRepositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<PagedResult<T>> GetAllByPageAsync(PaginationParameters paginationParameters);
        Task CreateAsync(T element);
        Task UpdateAsync(T element);
        Task DeleteAsync(int id);
        Task<bool> Exists(int id);
    }

}



