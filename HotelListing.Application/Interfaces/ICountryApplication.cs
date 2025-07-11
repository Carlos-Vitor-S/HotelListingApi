
using HotelListing.Application.DTOs.CountryDTOs;
using HotelListing.Domain.Models;

namespace HotelListing.Application.Interfaces
{
    public interface ICountryApplication
    {
        Task<GetCountryDto> Get(int id);
        Task<IEnumerable<GetCountryDto>> GetAllAsync();
        Task CreateAsync(CreateCountryDto createCountryDto);
        Task UpdateAsync(int id, UpdateCountryDto updateCountryDto);
        Task DeleteAsync(int id);
        Task<bool> Exists(int id);
        Task<GetCountryDetailsDto> GetDetails(int id);
        Task<PagedResult<GetCountryDto>> GetAllByPageAsync(PaginationParameters paginationParameters);
    }
}
