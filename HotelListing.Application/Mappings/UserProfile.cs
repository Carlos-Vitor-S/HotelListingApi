using AutoMapper;
using HotelListing.Application.DTOs.ApiAuthDTOs;
using HotelListing.Application.DTOs.UserDTOs;
using HotelListing.Domain.Models;

namespace HotelListing.Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, RegisterUserDto>().ReverseMap();
            CreateMap<User, LoginUserDto>().ReverseMap();
        }
    }
}
