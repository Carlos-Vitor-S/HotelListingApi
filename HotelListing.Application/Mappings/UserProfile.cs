using AutoMapper;
using HotelListing.Application.DTOs.ApiAuthDTOs;
using HotelListing.Application.DTOs.CountryDTOs;
using HotelListing.Application.DTOs.UserDTOs;
using HotelListing.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
