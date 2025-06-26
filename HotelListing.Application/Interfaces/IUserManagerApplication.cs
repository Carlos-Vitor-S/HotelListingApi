using HotelListing.Application.DTOs.ApiAuthDTOs;
using HotelListing.Application.DTOs.UserDTOs;
using HotelListing.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Application.Interfaces
{
    public interface IUserManagerApplication
    {
        Task<AuthResponse> LoginAsync(LoginUserDto loginUserDto);
        Task<AuthResponse> RegisterAsync(RegisterUserDto registerUserDto);
    }
}
