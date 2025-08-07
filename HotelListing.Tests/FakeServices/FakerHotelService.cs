using HotelListing.Domain.Exceptions;
using HotelListing.Domain.Interfaces.IServices;
using HotelListing.Domain.Models;

namespace HotelListing.Tests.FakeServices
{
    public class FakerHotelService : IHotelService
    {
        private readonly List<Hotel> _hotels;

        public FakerHotelService(List<Hotel>? hotels = null)
        {
            _hotels = hotels ?? new List<Hotel> {
                new Hotel
                {
                    Id = 1,
                    Name ="Hotel Nossa Senhora da Gloria",
                    Address ="231 Rua das Rosas, Nossa Senhora da Gloria, SE",
                    Rating = 5,
                    CountryId = 5
                },

                new Hotel
                {
                    Id = 14,
                    Name = "Hotel Paradise",
                    Address = "123 Rua das Flores, São Paulo, SP",
                    Rating = 4.5,
                    CountryId = 18
                },

                new Hotel
                {
                    Id = 7,
                    Name = "Hotel x",
                    Address = "Rua Y",
                    Rating = 3.5,
                    CountryId = 6
                }
            };

        }

        public Task CreateAsync(Hotel hotel)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Hotel> GetAllAsQueryable()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Hotel>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Hotel>>(_hotels);
        }

        public Task<Hotel> GetAsync(int id)
        {
            var hotel = _hotels.FirstOrDefault(h => h.Id == id);
            if (hotel == null)
            {
                throw new NotFoundCustomException(id.ToString(), "Hotel");
            }
            return Task.FromResult(hotel);
        }

        public Task UpdateAsync(Hotel hotel)
        {
            throw new NotImplementedException();
        }
    }
}
