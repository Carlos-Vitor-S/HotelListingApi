using HotelListing.Domain.Models;

namespace HotelListing.Domain.Interfaces.IServices
{
    public interface ICountryService
    {
        Task<Country> GetAsync(int id);
        Task<IEnumerable<Country>> GetAllAsync();
        IQueryable<Country> GetAllAsQueryable();
        Task CreateAsync(Country country);
        Task UpdateAsync(Country country);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<Country> GetDetailsAsync(int id);
    }
}
