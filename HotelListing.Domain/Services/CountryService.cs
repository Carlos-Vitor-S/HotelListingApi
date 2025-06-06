
using HotelListing.Domain.Exceptions.CountryExceptions;
using HotelListing.Domain.Interfaces;
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
                throw new CountryException("Country already exists and cannot be added again.");
            }

            await _countryRepository.CreateAsync(country);
        }

        public async Task DeleteAsync(int id)
        {
            var countryExists = await Exists(id);

            if (!countryExists)
            {
                throw new CountryException("Country doesnt exist and cannot be removed.");
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
                throw new CountryException("Country doesnt exist.");
            }
            return await _countryRepository.Get(id);
        }

        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            return await _countryRepository.GetAllAsync();
        }

        public async Task<Country> GetDetails(int id)
        {
            var countryExists = await Exists(id);

            if (!countryExists)
            {
                throw new CountryException("Country doesnt exist.");
            }

            return await _countryRepository.GetDetails(id);
        }

        public async Task UpdateAsync(Country country)
        {
            var countryExists = await Exists(country.Id);

            if (!countryExists)
            {
                throw new CountryException("Country doesnt exist and cannot be Udpated.");
            }

            await _countryRepository.UpdateAsync(country);
        }
    }
}
