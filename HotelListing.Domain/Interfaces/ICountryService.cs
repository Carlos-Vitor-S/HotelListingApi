using HotelListing.Domain.Models;

namespace HotelListing.Infra.Interfaces
{
    public interface ICountryService
    {
        Task<Country> GetById(int id);
        Task<IEnumerable<Country>> GetAll();
        Task Create(Country country);
        Task Update(Country country);
        Task Delete(int id);

    }
}
