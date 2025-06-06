using HotelListing.Domain.Models;

namespace HotelListing.Domain.Interfaces
{
    public interface ICountryRepository : IRepository<Country>
    {
        Task<Country> GetDetails(int id);
    }
}

