using AutoMapper;
using HotelListing.Application.DTOs.CountryDTOs;
using HotelListing.Application.Interfaces;
using HotelListing.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using HotelListing.Application.Utils;
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

        [Authorize(Roles = $"{Roles.Administrator},{Roles.NormalUser}")]
        [EnableQuery]
        [HttpGet]     
        public async Task<ActionResult<IEnumerable<GetCountryDto>>> GetAll()
        {
            var countriesDto = await _countryApplication.GetAllAsync();
            return Ok(countriesDto);
        }

        [Authorize(Roles = $"{Roles.Administrator},{Roles.NormalUser}")]
        [HttpGet("paged")]
        public async Task<ActionResult<PagedResult<GetCountryDto>>> GetAllByPage([FromQuery] PaginationParameters paginationParameters)
        {
            var pagedCountries = await _countryApplication.GetAllByPageAsync(paginationParameters);
            return Ok(pagedCountries);
        }

        [Authorize(Roles = $"{Roles.Administrator},{Roles.NormalUser}" )]
        [HttpGet("{id}")]      
        public async Task<ActionResult<GetCountryDetailsDto>> GetDetails(int id)
        {
            var country = await _countryApplication.GetDetails(id);
            return Ok(country);
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpPost]
        public async Task<ActionResult<CreateCountryDto>> Create([FromBody] CreateCountryDto createCountryDto)
        {
            await _countryApplication.CreateAsync(createCountryDto);
            return Created("Country Created", createCountryDto);
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpPut("{id}")]       
        public async Task<ActionResult<UpdateCountryDto>> Update(int id, [FromBody] UpdateCountryDto updateCountryDto)
        {
            await _countryApplication.UpdateAsync(id, updateCountryDto);
            return Ok(updateCountryDto);
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpDelete("{id}")]        
        public async Task<ActionResult<UpdateCountryDto>> Delete(int id)
        {
            await _countryApplication.DeleteAsync(id);
            return NoContent();
        }
    }
}
