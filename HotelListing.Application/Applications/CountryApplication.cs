using AutoMapper;
using HotelListing.Application.DTOs.CountryDTOs;
using HotelListing.Application.Interfaces;
using HotelListing.Application.Models;
using HotelListing.Domain.Interfaces.IServices;
using HotelListing.Domain.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> ExistsAsync(int id)
        {
            return await _countryService.ExistsAsync(id);
        }

        public async Task<GetCountryDto> GetAsync(int id)
        {
            var country = await _countryService.GetAsync(id);
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
            var skipCount = (paginationParameters.PageNumber - 1) * paginationParameters.PageSize;
            var query = _countryService.GetAllAsQueryable();
            var totalItems = await query.CountAsync();

            var pagedCountryItems = await query
                .Skip(skipCount)
                .Take(paginationParameters.PageSize)
                .ToListAsync();

            var pagedCountryItemsDto = _mapper.Map<List<GetCountryDto>>(pagedCountryItems);

            return new PagedResult<GetCountryDto>
            {
                Items = pagedCountryItemsDto,
                TotalItems = totalItems,
                CurrentPage = paginationParameters.PageNumber,
                PageSize = paginationParameters.PageSize
            };
        }

        public async Task<GetCountryDetailsDto> GetDetailsAsync(int id)
        {
            var country = await _countryService.GetDetailsAsync(id);
            var countryDto = _mapper.Map<GetCountryDetailsDto>(country);
            return countryDto;
        }

        public async Task UpdateAsync(int id, UpdateCountryDto updateCountryDto)
        {
            var country = await _countryService.GetAsync(id);
            _mapper.Map(updateCountryDto, country);
            await _countryService.UpdateAsync(country);
        }
    }
}
