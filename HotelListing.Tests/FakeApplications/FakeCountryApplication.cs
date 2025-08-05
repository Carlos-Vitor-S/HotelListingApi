using HotelListing.Application.DTOs.CountryDTOs;
using HotelListing.Application.Interfaces;
using HotelListing.Application.Models;

namespace HotelListing.Tests.FakeApplications
{
    public class FakeCountryApplication : ICountryApplication
    {
        private readonly List<GetCountryDto> _countries;
        public FakeCountryApplication()
        {
            _countries = new List<GetCountryDto>
            {
                new GetCountryDto { Id = 1, Name = "Brasil", ShortName = "BR" },
                new GetCountryDto { Id = 2, Name = "Argentina", ShortName = "AR" },
                new GetCountryDto { Id = 3, Name = "Chile", ShortName = "CL" },
            };
        }

        public Task<PagedResult<GetCountryDto>> GetAllByPageAsync(PaginationParameters paginationParameters)
        {
            var skipCount = (paginationParameters.PageNumber - 1) * paginationParameters.PageSize;

            var pagedCountryItems = _countries
                .Skip(skipCount)
                .Take(paginationParameters.PageSize)
                .ToList();

            var totalItems = _countries.Count;

            var pagedResult = new PagedResult<GetCountryDto>
            {
                Items = pagedCountryItems,
                TotalItems = totalItems,
                CurrentPage = paginationParameters.PageNumber,
                PageSize = paginationParameters.PageSize
            };

            return Task.FromResult(pagedResult);
        }

        public Task CreateAsync(CreateCountryDto createCountryDto)
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

        public Task<IEnumerable<GetCountryDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GetCountryDto> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GetCountryDetailsDto> GetDetailsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, UpdateCountryDto updateCountryDto)
        {
            throw new NotImplementedException();
        }
    }
}
