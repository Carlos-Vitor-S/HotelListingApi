using HotelListing.Domain.Models;

namespace HotelListing.Domain.Interfaces.IServices
{
    public interface IHotelService
    {
        Task<Hotel> Get(int id);
        Task<IEnumerable<Hotel>> GetAllAsync();
        IQueryable<Hotel> GetAllAsQueryable();
        Task CreateAsync(Hotel hotel);
        Task UpdateAsync(Hotel hotel);
        Task DeleteAsync(int id);
        Task<bool> Exists(int id);
    }
}
