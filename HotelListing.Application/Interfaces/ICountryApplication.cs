using HotelListing.Domain.Models;

namespace HotelListing.Application.Interfaces
{
    public interface ICountryApplication
    {
        Task<Country> Get(int id);
        Task<IEnumerable<Country>> GetAllAsync();
        Task CreateAsync(Country country);
        Task UpdateAsync(Country country);
        Task DeleteAsync(int id);
        Task<bool> Exists(int id);
        Task<Country> GetDetails(int id);
    }
}
