using HotelListing.Domain.Models;

namespace HotelListing.Domain.Interfaces.IRepositories
{
    public interface ICountryRepository : IRepository<Country>
    {
        Task<Country> GetDetailsAsync(int id);
    }
}

