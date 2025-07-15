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

        private readonly IMapper _mapper;
        private readonly IUserManagerApplication _authManagerApplication;

        public UserManagerController(IMapper mapper, IUserManagerApplication authManagerApplication)
        {
            _mapper = mapper;
            _authManagerApplication = authManagerApplication;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto , [FromQuery] string role)
        {
            try
            {
                var authResponse = await _authManagerApplication.RegisterAsync(registerUserDto , role);
                return Ok(authResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            try
            {
                var authResponse = await _authManagerApplication.LoginAsync(loginUserDto);
                return Ok(authResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }



    }
}
