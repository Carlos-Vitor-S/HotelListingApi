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

        public CountriesController(ICountryApplication countryApplication)
        {
            _countryApplication = countryApplication;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Country>>> GetAll() { 
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
        public async Task<ActionResult<Country>> Create([FromBody] Country country)
        { 
            await _countryApplication.Create(country);
            return Ok(country);
            
        }

    }
}
