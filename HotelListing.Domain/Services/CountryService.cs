using HotelListing.Domain.Exceptions.CountryExceptions;
using HotelListing.Domain.Models;
using HotelListing.Infra.Interfaces;

namespace HotelListing.Domain.Services
{
    public class CountryService : ICountryService
    {
        IRepository<Country> _countryRepository;

        public CountryService(IRepository<Country> countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task Create(Country country)
        {
            var existingCountry = await _countryRepository.GetById(country.Id);

            if (existingCountry != null)
            {
                throw new CountryException($"Country already exists and cannot be created again");
            }

            await _countryRepository.Create(country);
        }

        public Task Delete(int id)
        {

            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Country>> GetAll()
        {
            return await _countryRepository.GetAll();
        }

        public async Task<Country> GetById(int id)
        {
            return await _countryRepository.GetById(id);
        }

        public Task Update(Country country)
        {
            throw new NotImplementedException();
        }
    }
}
