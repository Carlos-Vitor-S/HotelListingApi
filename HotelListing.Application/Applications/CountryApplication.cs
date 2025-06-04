using HotelListing.Application.Interfaces;
using HotelListing.Domain.Models;
using HotelListing.Infra.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Application.Applications
{
    public class CountryApplication : ICountryApplication
    {
        private readonly ICountryService _countryService;

        public CountryApplication(ICountryService countryService)
        {
            _countryService = countryService;
        }

        public async Task Create(Country country)
        {
            await _countryService.Create(country);
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Country>> GetAll()
        {
           return await _countryService.GetAll();
        }

        public async Task<Country> GetById(int id)
        {
            return await _countryService.GetById(id);
        }

        public Task Update(Country country)
        {
            throw new NotImplementedException();
        }

      
    }
}
