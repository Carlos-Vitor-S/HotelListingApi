using AutoMapper;
using HotelListing.Application.DTOs.HotelDTOs;
using HotelListing.Application.Interfaces;
using HotelListing.Application.Models;
using HotelListing.Domain.Interfaces.IServices;
using HotelListing.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace HotelListing.Application.Applications
{
    public class HotelApplication : IHotelApplication
    {
        private readonly IHotelService _hotelService;
        private readonly IMapper _mapper;

        public HotelApplication(IHotelService hotelService, IMapper mapper)
        {
            _hotelService = hotelService;
            _mapper = mapper;
        }

        public async Task CreateAsync(CreateHotelDto createHotelDto)
        {
            var hotelDto = _mapper.Map<Hotel>(createHotelDto);
            await _hotelService.CreateAsync(hotelDto);
        }

        public async Task DeleteAsync(int id)
        {
            await _hotelService.DeleteAsync(id);
        }

        public async Task<bool> Exists(int id)
        {
            return await _hotelService.Exists(id);
        }

        public async Task<GetHotelDto> Get(int id)
        {
            var hotel = await _hotelService.Get(id);
            var hotelDto = _mapper.Map<GetHotelDto>(hotel);
            return hotelDto;
        }

        public async Task<IEnumerable<GetHotelDto>> GetAllAsync()
        {
            var hotels = await _hotelService.GetAllAsync();
            var hotelsDto = _mapper.Map<List<GetHotelDto>>(hotels);
            return hotelsDto;
        }

        public async Task<PagedResult<GetHotelDto>> GetAllByPageAsync(PaginationParameters paginationParameters)
        {
            var query = _hotelService.GetAllAsQueryable();
            var skipCount = (paginationParameters.PageNumber - 1) * paginationParameters.PageSize;
            var totalItems = await query.CountAsync();

            var pagedHotelItems = await query
                .Skip(skipCount)
                .Take(paginationParameters.PageSize)
                .ToListAsync();

            var pagedHotelItensDto = _mapper.Map<List<GetHotelDto>>(pagedHotelItems);

            return new PagedResult<GetHotelDto>
            {
                Items = pagedHotelItensDto,
                TotalItems = totalItems,
                CurrentPage = paginationParameters.PageNumber,
                PageSize = paginationParameters.PageSize
            };

        }

        public async Task UpdateAsync(int id, UpdateHotelDto updateHotelDto)
        {
            var hotel = await _hotelService.Get(id);
            _mapper.Map(updateHotelDto, hotel);
            await _hotelService.UpdateAsync(hotel);
        }
    }
}
