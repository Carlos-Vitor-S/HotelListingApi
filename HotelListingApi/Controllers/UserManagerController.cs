using AutoMapper;
using HotelListing.Application.DTOs.ApiAuthDTOs;
using HotelListing.Application.DTOs.UserDTOs;
using HotelListing.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserManagerController : ControllerBase
    {

        private readonly IUserManagerApplication _authManagerApplication;

        public UserManagerController(IUserManagerApplication authManagerApplication)
        {
            _authManagerApplication = authManagerApplication;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto, [FromQuery] string role)
        {
            var authResponse = await _authManagerApplication.RegisterAsync(registerUserDto, role);
            return Ok(authResponse);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            var authResponse = await _authManagerApplication.LoginAsync(loginUserDto);
            return Ok(authResponse);
        }
    }
}
