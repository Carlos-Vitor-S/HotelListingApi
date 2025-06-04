using AutoMapper;
using HotelListing.Application.DTOs.CountryDTOs;
using HotelListing.Application.Interfaces;
using HotelListing.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CountriesController : ControllerBase
    {

        private readonly ICountryApplication _countryApplication;
        private readonly IMapper _countryProfile;
        public CountriesController(ICountryApplication countryApplication, IMapper countryProfile)
        {
            _countryApplication = countryApplication;
            _countryProfile = countryProfile;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Country>>> GetAll()
        {
            var allCountries = await _countryApplication.GetAll();
            return Ok(allCountries);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Country>> GetById(int id)
        {
            var countryById = await _countryApplication.GetById(id);
            return Ok(countryById);
        }

        [HttpPost]
        public async Task<ActionResult<Country>> Create([FromBody] CreateCountryDto createCountryDto)
        {
            var country = _countryProfile.Map<Country>(createCountryDto);

            await _countryApplication.Create(country);
            return CreatedAtAction("GetById", new { id = country.Id }, country);
        }

    }
}
