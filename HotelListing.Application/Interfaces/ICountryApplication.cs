
using HotelListing.Application.DTOs.CountryDTOs;
using HotelListing.Application.Models;

namespace HotelListing.Application.Interfaces
{
    public interface ICountryApplication
    {
        Task<GetCountryDto> GetAsync(int id);
        Task<IEnumerable<GetCountryDto>> GetAllAsync();
        Task CreateAsync(CreateCountryDto createCountryDto);
        Task UpdateAsync(int id, UpdateCountryDto updateCountryDto);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<GetCountryDetailsDto> GetDetailsAsync(int id);
        Task<PagedResult<GetCountryDto>> GetAllByPageAsync(PaginationParameters paginationParameters);
    }
}
