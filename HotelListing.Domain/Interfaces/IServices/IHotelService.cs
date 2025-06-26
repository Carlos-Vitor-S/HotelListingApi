using HotelListing.Domain.Interfaces.IRepositories;
using HotelListing.Domain.Models;

namespace HotelListing.Domain.Interfaces.IServices
{
    public interface IHotelService : IRepository<Hotel>
    {
        Task<Hotel> Get(int id);
        Task<IEnumerable<Hotel>> GetAllAsync();
        Task CreateAsync(Hotel hotel);
        Task UpdateAsync(Hotel hotel);
        Task DeleteAsync(int id);
        Task<bool> Exists(int id);
    }
}
