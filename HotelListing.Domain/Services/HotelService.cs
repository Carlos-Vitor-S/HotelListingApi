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
                throw new ConflictCustomException(key: hotel.Id.ToString(), name: "Hotel");
            }

            await _hotelRepository.CreateAsync(hotel);
        }

        public async Task DeleteAsync(int id)
        {
            var hotelExists = await Exists(id);

            if (!hotelExists)
            {
                throw new NotFoundCustomException(key: id.ToString(), name: "Hotel");
            }

            await _hotelRepository.DeleteAsync(id);
        }

        public async Task<bool> Exists(int id)
        {
            return await _hotelRepository.Exists(id);
        }

        public async Task<Hotel> Get(int id)
        {
            var hotelExists = await _hotelRepository.Exists(id);

            if (!hotelExists)
            {
                throw new NotFoundCustomException(key: id.ToString(), name: "Hotel");
            }

            return await _hotelRepository.Get(id);
        }

        public async Task<IEnumerable<Hotel>> GetAllAsync()
        {
            return await _hotelRepository.GetAllAsync();
        }

        public async Task<PagedResult<Hotel>> GetAllByPageAsync(PaginationParameters paginationParameters)
        {
            return await _hotelRepository.GetAllByPageAsync(paginationParameters);
        }

        public async Task UpdateAsync(Hotel hotel)
        {
            var hotelExists = await _hotelRepository.Exists(hotel.Id);

            if (!hotelExists)
            {
                throw new NotFoundCustomException(key: hotel.Id.ToString(), name: "Hotel");
            }

            await _hotelRepository.UpdateAsync(hotel);
        }
    }
}
