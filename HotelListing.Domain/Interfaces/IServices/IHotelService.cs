using HotelListing.Domain.Models;

namespace HotelListing.Domain.Interfaces.IServices
{
    public interface IHotelService
    {
        Task<Hotel> GetAsync(int id);
        Task<IEnumerable<Hotel>> GetAllAsync();
        IQueryable<Hotel> GetAllAsQueryable();
        Task CreateAsync(Hotel hotel);
        Task UpdateAsync(Hotel hotel);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
