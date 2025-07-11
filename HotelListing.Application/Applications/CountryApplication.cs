using HotelListing.Application.Interfaces;
using HotelListing.Domain.Interfaces.IServices;
using HotelListing.Domain.Models;

namespace HotelListing.Application.Applications
{
    public class CountryApplication : ICountryApplication
    {
        private readonly ICountryService _countryService;

        public CountryApplication(ICountryService countryService)
        {
            _countryService = countryService;
        }

        public async Task CreateAsync(Country country)
        {
            await _countryService.CreateAsync(country);
        }

        public async Task DeleteAsync(int id)
        {

            await _countryService.DeleteAsync(id);
        }

        public async Task<bool> Exists(int id)
        {
            return await _countryService.Exists(id);
        }

        public async Task<Country> Get(int id)
        {
            return await _countryService.Get(id);
        }

        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            return await _countryService.GetAllAsync();
        }

        public async Task<PagedResult<Country>> GetAllByPageAsync(PaginationParameters paginationParameters)
        {
            return await _countryService.GetAllByPageAsync(paginationParameters);
        }

        public async Task<Country> GetDetails(int id)
        {
            return await _countryService.GetDetails(id);
        }

        public async Task UpdateAsync(Country country)
        {
            await _countryService.UpdateAsync(country);
        }
    }
}
