using HotelListing.Application.DTOs.CountryDTOs;
using HotelListing.Application.Interfaces;
using HotelListing.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
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


        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountryDto>>> GetAll()
        {
            var countriesDto = await _countryApplication.GetAllAsync();
            return Ok(countriesDto);
        }


        [HttpGet("paged")]
        public async Task<ActionResult<PagedResult<GetCountryDto>>> GetAllByPage([FromQuery] PaginationParameters paginationParameters)
        {
            var pagedCountries = await _countryApplication.GetAllByPageAsync(paginationParameters);
            return Ok(pagedCountries);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<GetCountryDetailsDto>> GetDetails(int id)
        {
            var country = await _countryApplication.GetDetails(id);
            return Ok(country);
        }


        [HttpPost]
        public async Task<ActionResult<CreateCountryDto>> Create([FromBody] CreateCountryDto createCountryDto)
        {
            await _countryApplication.CreateAsync(createCountryDto);
            return Created("Country Created", createCountryDto);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateCountryDto>> Update(int id, [FromBody] UpdateCountryDto updateCountryDto)
        {
            await _countryApplication.UpdateAsync(id, updateCountryDto);
            return Ok(updateCountryDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UpdateCountryDto>> Delete(int id)
        {
            await _countryApplication.DeleteAsync(id);
            return NoContent();
        }
    }
}
