using HotelListing.Domain.Exceptions;
using HotelListing.Domain.Interfaces.IRepositories;
using HotelListing.Domain.Interfaces.IServices;
using HotelListing.Domain.Models;

namespace HotelListing.Domain.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task CreateAsync(Country country)
        {
            var countryExists = await Exists(country.Id);

            if (countryExists)
            {
                throw new ConflictCustomException(key: country.Id.ToString(), name: "Country");
            }

            await _countryRepository.CreateAsync(country);
        }

        public async Task DeleteAsync(int id)
        {
            var countryExists = await Exists(id);

            if (!countryExists)
            {
                throw new NotFoundCustomException(key: id.ToString(), name: "Country");
            }

            await _countryRepository.DeleteAsync(id);
        }

        public async Task<bool> Exists(int id)
        {
            return await _countryRepository.Exists(id);
        }

        public async Task<Country> Get(int id)
        {
            var countryExists = await Exists(id);

            if (!countryExists)
            {
                throw new NotFoundCustomException(key: id.ToString(), name: "Country");
            }
            return await _countryRepository.Get(id);
        }

        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            return await _countryRepository.GetAllAsync();
        }

        public async Task<PagedResult<Country>> GetAllByPageAsync(PaginationParameters paginationParameters)
        {
            return await _countryRepository.GetAllByPageAsync(paginationParameters);
        }

        public async Task<Country> GetDetails(int id)
        {
            var countryExists = await Exists(id);

            if (!countryExists)
            {
                throw new NotFoundCustomException(key: id.ToString(), name: "Country");
            }

            return await _countryRepository.GetDetails(id);
        }

        public async Task UpdateAsync(Country country)
        {
            var countryExists = await Exists(country.Id);

            if (!countryExists)
            {
                throw new NotFoundCustomException(key: country.Id.ToString(), name: "Country");
            }

            await _countryRepository.UpdateAsync(country);
        }
    }
}
