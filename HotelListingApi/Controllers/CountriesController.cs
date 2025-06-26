using AutoMapper;
using HotelListing.Application.DTOs.CountryDTOs;
using HotelListing.Application.Interfaces;
using HotelListing.Domain.Exceptions;
using HotelListing.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace HotelListing.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CountriesController : ControllerBase
    {

        private readonly ICountryApplication _countryApplication;
        private readonly IMapper _mapper;

        public CountriesController(ICountryApplication countryApplication, IMapper mapper)
        {
            _countryApplication = countryApplication;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountryDto>>> GetAll()
        {
            try
            {
                var countries = await _countryApplication.GetAllAsync();
                var countriesDto = _mapper.Map<List<GetCountryDto>>(countries);
                return Ok(countriesDto);
            }
            catch (CountryException countryException)
            {
                return NotFound(countryException.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCountryDetailsDto>> GetDetails(int id)
        {
            try
            {
                var country = await _countryApplication.GetDetails(id);
                var countryDto = _mapper.Map<GetCountryDetailsDto>(country);
                return Ok(countryDto);
            }
            catch (CountryException countryException)
            {
                return NotFound(countryException.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CreateCountryDto>> Create([FromBody] CreateCountryDto createCountryDto)
        {
            try
            {
                var country = _mapper.Map<Country>(createCountryDto);
                await _countryApplication.CreateAsync(country);
                return CreatedAtAction(nameof(GetDetails), new { id = country.Id }, country);
            }
            catch (CountryException countryException)
            {
                return Conflict(countryException.Message);
            }
        }
    }
}
