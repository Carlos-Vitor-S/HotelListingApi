using AutoMapper;
using HotelListing.Application.DTOs.CountryDTOs;
using HotelListing.Application.Interfaces;
using HotelListing.Domain.Interfaces.IServices;
using HotelListing.Domain.Models;

namespace HotelListing.Application.Applications
{
    public class CountryApplication : ICountryApplication
    {
        private readonly ICountryService _countryService;
        private readonly IMapper _mapper;

        public CountryApplication(ICountryService countryService, IMapper mapper)
        {
            _countryService = countryService;
            _mapper = mapper;
        }

        public async Task CreateAsync(CreateCountryDto createCountryDto)
        {
            var country = _mapper.Map<Country>(createCountryDto);
            await _countryService.CreateAsync(country);
        }
        public async Task DeleteAsync(int id)
        {
            await _countryService.DeleteAsync(id);
        }

        public async Task<bool> Exists(int id)
        {
            return await _countryService.Exists(id);
        }

        public async Task<GetCountryDto> Get(int id)
        {
            var country = await _countryService.Get(id);
            var countryDto = _mapper.Map<GetCountryDto>(country);
            return countryDto;
        }

        public async Task<IEnumerable<GetCountryDto>> GetAllAsync()
        {
            var countries = await _countryService.GetAllAsync();
            var countriesDto = _mapper.Map<List<GetCountryDto>>(countries);
            return countriesDto;
        }

        public async Task<PagedResult<GetCountryDto>> GetAllByPageAsync(PaginationParameters paginationParameters)
        {
            var pagedCountries = await _countryService.GetAllByPageAsync(paginationParameters);

            var pagedCountriesDto = new PagedResult<GetCountryDto>
            {
                Items = _mapper.Map<List<GetCountryDto>>(pagedCountries.Items),
                TotalItems = pagedCountries.TotalItems,
                CurrentPage = pagedCountries.CurrentPage,
                PageSize = pagedCountries.PageSize
            };

            return pagedCountriesDto;
        }

        public async Task<GetCountryDetailsDto> GetDetails(int id)
        {
            var country = await _countryService.GetDetails(id);
            var countryDto = _mapper.Map<GetCountryDetailsDto>(country);
            return countryDto;
        }

        public async Task UpdateAsync(int id, UpdateCountryDto updateCountryDto)
        {
            var country = await _countryService.Get(id);
            _mapper.Map(updateCountryDto, country);
            await _countryService.UpdateAsync(country);
        }
    }
}
