using HotelListing.Application.DTOs.HotelDTOs;
using HotelListing.Application.Models;

namespace HotelListing.Application.Interfaces
{
    public interface IHotelApplication
    {
        Task<GetHotelDto> GetAsync(int id);
        Task<IEnumerable<GetHotelDto>> GetAllAsync();
        Task CreateAsync(CreateHotelDto createHotelDto);
        Task UpdateAsync(int id, UpdateHotelDto updateHotelDto);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<PagedResult<GetHotelDto>> GetAllByPageAsync(PaginationParameters paginationParameters);
    }
}
