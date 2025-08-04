using AutoMapper;
using HotelListing.Application.DTOs.ApiAuthDTOs;
using HotelListing.Application.DTOs.UserDTOs;
using HotelListing.Application.Interfaces;
using HotelListing.Domain.Interfaces.IServices;
using HotelListing.Domain.Models;


namespace HotelListing.Application.Applications
{
    public class UserManagerApplication : IUserManagerApplication
    {
        private readonly IUserManagerService _userManagerService;
        private readonly IMapper _mapper;

        public UserManagerApplication(IUserManagerService userManagerService, IMapper mapper)
        {
            _userManagerService = userManagerService;
            _mapper = mapper;
        }

        public async Task<AuthResponse> LoginAsync(LoginUserDto loginUserDto)
        {
            var user = _mapper.Map<User>(loginUserDto);

            return await _userManagerService.LoginAsync(user);
        }

        public async Task<AuthResponse> RegisterAsync(RegisterUserDto registerUserDto, string role)
        {
            var user = _mapper.Map<User>(registerUserDto);

            return await _userManagerService.RegisterAsync(user, role);
        }
    }
}
