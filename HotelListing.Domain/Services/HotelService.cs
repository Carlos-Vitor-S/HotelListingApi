using HotelListing.Domain.Exceptions;
using HotelListing.Domain.Interfaces.IRepositories;
using HotelListing.Domain.Interfaces.IServices;
using HotelListing.Domain.Models;

namespace HotelListing.Domain.Services
{
    public class HotelService : IHotelService
    {
        private readonly IRepository<Hotel> _hotelRepository;

        public HotelService(IRepository<Hotel> hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task CreateAsync(Hotel hotel)
        {
            var hotelExists = await Exists(hotel.Id);

            if (hotelExists)
            {
                throw new HotelException("Hotel already exists and cannot be added again.");
            }

            await _hotelRepository.CreateAsync(hotel);
        }

        public async Task DeleteAsync(int id)
        {
            await _hotelRepository.DeleteAsync(id);
        }

        public async Task<bool> Exists(int id)
        {
            return await _hotelRepository.Exists(id);
        }

        public async Task<Hotel> Get(int id)
        {
            var hotelExists = await Exists(id);

            if (!hotelExists)
            {
                throw new HotelException("Hotel doesnt exist.");
            }
            return await _hotelRepository.Get(id);
        }

        public async Task<IEnumerable<Hotel>> GetAllAsync()
        {
            return await _hotelRepository.GetAllAsync();
        }


        public async Task UpdateAsync(Hotel hotel)
        {
            await _hotelRepository.UpdateAsync(hotel);
        }
    }
}
