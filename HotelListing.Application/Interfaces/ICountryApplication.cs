using HotelListing.Domain.Models;

namespace HotelListing.Application.Interfaces
{
    public interface ICountryApplication
    {
        Task<Country> GetById(int id);
        Task<IEnumerable<Country>> GetAll();
        Task Create(Country country);
        Task Update(Country country);
        Task Delete(int id);
    }
}
