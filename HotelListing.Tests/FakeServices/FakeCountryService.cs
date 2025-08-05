using HotelListing.Domain.Exceptions;
using HotelListing.Domain.Interfaces.IServices;
using HotelListing.Domain.Models;
using System.Diagnostics.Metrics;

namespace HotelListing.Tests.FakeRepository
{
    public class FakeCountryService : ICountryService
    {
        private readonly List<Country> _countries;
        private readonly List<Hotel> _hotels;

        public FakeCountryService(List<Country>? countries = null, List<Hotel>? hotels = null)
        {
            _countries = countries ?? new List<Country>
            {
                new Country { Id = 1, Name = "Brasil", ShortName = "BR" },
                new Country { Id = 2, Name = "Argentina", ShortName = "AR" },
                new Country { Id = 3, Name = "Chile", ShortName = "CL" }
            };
            _hotels = hotels ?? new List<Hotel>
            {
                new Hotel { Id = 1, Name = "Hotel Paraíso", Address = "Rua A, 123", Rating = 4.2, CountryId = 1 },
                new Hotel { Id = 2, Name = "Hotel Central", Address = "Avenida B, 456", Rating = 3.8, CountryId = 1 },
                new Hotel { Id = 3, Name = "Pousada Sol", Address = "Rua C, 789", Rating = 4.0, CountryId = 2 }
            };

        }

        public Task<Country> GetAsync(int id)
        {
            var country = _countries.FirstOrDefault(c => c.Id == id);
            if (country == null)
            {
                throw new NotFoundCustomException(id.ToString(), "Country");
            }
            return Task.FromResult(country);
        }

        public Task CreateAsync(Country country)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var element = await GetAsync(id);
            return element != null;
        }

        public IQueryable<Country> GetAllAsQueryable()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            return await Task.FromResult<IEnumerable<Country>>(_countries);
        }

        public async Task<Country> GetDetailsAsync(int id)
        {
            var countryExists = await ExistsAsync(id);

            if (!countryExists)
            {
                throw new NotFoundCustomException(key: id.ToString(), name: "Country");
            }

            var countries = _countries.First(c => c.Id == id);
            countries.Hotels = _hotels.Where(h => h.CountryId == id).ToList();
            return countries;

        }

        public Task UpdateAsync(Country country)
        {
            throw new NotImplementedException();
        }
    }
}
