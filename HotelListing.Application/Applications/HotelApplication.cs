using AutoMapper;
using HotelListing.Application.DTOs.HotelDTOs;
using HotelListing.Application.Interfaces;
using HotelListing.Domain.Interfaces.IServices;
using HotelListing.Domain.Models;

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

        public async Task UpdateAsync(int id, UpdateHotelDto updateHotelDto)
        {
            var hotel = await _hotelService.Get(id);
            _mapper.Map(updateHotelDto, hotel);
            await _hotelService.UpdateAsync(hotel);
        }
    }
}
