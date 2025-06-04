using AutoMapper;
using HotelListing.Application.DTOs.CountryDTOs;
using HotelListing.Domain.Models;

namespace HotelListing.Application.Mappings
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<Country, CreateCountryDto>().ReverseMap();
        }

    }
}
