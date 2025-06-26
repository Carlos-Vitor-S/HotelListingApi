using HotelListing.Application.Interfaces;
using HotelListing.Domain.Interfaces.IServices;
using HotelListing.Domain.Models;

namespace HotelListing.Application.Applications
{
    public class HotelApplication : IHotelApplication
    {
        private readonly IHotelService _hotelService;

        public HotelApplication(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        public async Task CreateAsync(Hotel hotel)
        {
            await _hotelService.CreateAsync(hotel);
        }

        public async Task DeleteAsync(int id)
        {
            await _hotelService.DeleteAsync(id);
        }

        public async Task<bool> Exists(int id)
        {
            return await _hotelService.Exists(id);
        }

        public async Task<Hotel> Get(int id)
        {
            return await _hotelService.Get(id);
        }

        public async Task<IEnumerable<Hotel>> GetAllAsync()
        {
            return await _hotelService.GetAllAsync();
        }

        public async Task UpdateAsync(Hotel hotel)
        {
            await _hotelService.UpdateAsync(hotel);
        }
    }
}
