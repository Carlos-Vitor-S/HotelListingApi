using HotelListing.Application.DTOs.ApiAuthDTOs;
using HotelListing.Application.DTOs.UserDTOs;
using HotelListing.Domain.Models;

namespace HotelListing.Application.Interfaces
{
    public interface IUserManagerApplication
    {
        Task<AuthResponse> LoginAsync(LoginUserDto loginUserDto);
        Task<AuthResponse> RegisterAsync(RegisterUserDto registerUserDto , string role);
    }
}
