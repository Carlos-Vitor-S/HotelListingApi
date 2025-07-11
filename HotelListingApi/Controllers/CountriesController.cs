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

            var countries = await _countryApplication.GetAllAsync();
            var countriesDto = _mapper.Map<List<GetCountryDto>>(countries);
            return Ok(countriesDto);
        }

        [HttpGet("paged")]
        public async Task<ActionResult<PagedResult<GetCountryDto>>> GetAllByPage([FromQuery] PaginationParameters paginationParameters)
        {
            var pagedCountries = await _countryApplication.GetAllByPageAsync(paginationParameters);

            var pagedCountriesDto = new PagedResult<GetCountryDto>
            {
                Items = _mapper.Map<List<GetCountryDto>>(pagedCountries.Items),
                TotalItems = pagedCountries.TotalItems,
                CurrentPage = pagedCountries.CurrentPage,
                PageSize = pagedCountries.PageSize
            };

            return Ok(pagedCountriesDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCountryDetailsDto>> GetDetails(int id)
        {
            var country = await _countryApplication.GetDetails(id);
            var countryDto = _mapper.Map<GetCountryDetailsDto>(country);
            return Ok(countryDto);
        }

        [HttpPost]

        public async Task<ActionResult<CreateCountryDto>> Create([FromBody] CreateCountryDto createCountryDto)
        {

            var country = _mapper.Map<Country>(createCountryDto);
            await _countryApplication.CreateAsync(country);
            return CreatedAtAction(nameof(GetDetails), new { id = country.Id }, country);


        }
    }
}
