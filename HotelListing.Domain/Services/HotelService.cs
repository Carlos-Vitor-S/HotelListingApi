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
            var hotelExists = await ExistsAsync(hotel.Id);

            if (hotelExists)
            {
                throw new ConflictCustomException(key: hotel.Id.ToString(), name: "Hotel");
            }

            await _hotelRepository.CreateAsync(hotel);
        }

        public async Task DeleteAsync(int id)
        {
            var hotelExists = await ExistsAsync(id);

            if (!hotelExists)
            {
                throw new NotFoundCustomException(key: id.ToString(), name: "Hotel");
            }

            await _hotelRepository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _hotelRepository.ExistsAsync(id);
        }

        public async Task<Hotel> GetAsync(int id)
        {
            var hotelExists = await _hotelRepository.ExistsAsync(id);

            if (!hotelExists)
            {
                throw new NotFoundCustomException(key: id.ToString(), name: "Hotel");
            }

            return await _hotelRepository.GetAsync(id);
        }

        public IQueryable<Hotel> GetAllAsQueryable()
        {
            return _hotelRepository.GetAllAsQueryable();
        }

        public async Task<IEnumerable<Hotel>> GetAllAsync()
        {
            return await _hotelRepository.GetAllAsync();
        }

        public async Task UpdateAsync(Hotel hotel)
        {
            var hotelExists = await _hotelRepository.ExistsAsync(hotel.Id);

            if (!hotelExists)
            {
                throw new NotFoundCustomException(key: hotel.Id.ToString(), name: "Hotel");
            }

            await _hotelRepository.UpdateAsync(hotel);
        }
    }
}
