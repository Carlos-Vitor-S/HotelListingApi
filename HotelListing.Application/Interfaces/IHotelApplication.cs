using HotelListing.Domain.Models;

namespace HotelListing.Application.Interfaces
{
    public interface IHotelApplication
    {
        Task<Hotel> Get(int id);
        Task<IEnumerable<Hotel>> GetAllAsync();
        Task CreateAsync(Hotel hotel);
        Task UpdateAsync(Hotel hotel);
        Task DeleteAsync(int id);
        Task<bool> Exists(int id);
    }
}
