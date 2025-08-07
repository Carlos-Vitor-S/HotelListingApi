using HotelListing.Application.DTOs.HotelDTOs;
using HotelListing.Application.Interfaces;
using HotelListing.Application.Models;

namespace HotelListing.Tests.FakeApplications
{
    public class FakerHotelApplication : IHotelApplication
    {
        private readonly List<GetHotelDto> _hotels;
        public FakerHotelApplication()
        {
            _hotels = new List<GetHotelDto> {
                new GetHotelDto
                {
                    Id = 1,
                    Name ="Hotel Nossa Senhora da Gloria",
                    Address ="231 Rua das Rosas, Nossa Senhora da Gloria, SE",
                    Rating = 5,
                    CountryId = 5
                },

                new GetHotelDto
                {
                    Id = 14,
                    Name = "Hotel Paradise",
                    Address = "123 Rua das Flores, São Paulo, SP",
                    Rating = 4.5,
                    CountryId = 18
                },

                new GetHotelDto
                {
                    Id = 7,
                    Name = "Hotel x",
                    Address = "Rua Y",
                    Rating = 3.5,
                    CountryId = 6
                }
            };
        }
        public Task CreateAsync(CreateHotelDto createHotelDto)
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

        public Task<IEnumerable<GetHotelDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<GetHotelDto>> GetAllByPageAsync(PaginationParameters paginationParameters)
        {
            var skipCount = (paginationParameters.PageNumber - 1) * paginationParameters.PageSize;

            var pagedCountryItems = _hotels
                .Skip(skipCount)
                .Take(paginationParameters.PageSize)
                .ToList();

            var totalItems = _hotels.Count;

            var pagedResult = new PagedResult<GetHotelDto>
            {
                Items = pagedCountryItems,
                TotalItems = totalItems,
                CurrentPage = paginationParameters.PageNumber,
                PageSize = paginationParameters.PageSize
            };

            return Task.FromResult(pagedResult);
        }

        public Task<GetHotelDto> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, UpdateHotelDto updateHotelDto)
        {
            throw new NotImplementedException();
        }
    }
}
