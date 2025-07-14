using AutoMapper;
using HotelListing.Application.DTOs.HotelDTOs;
using HotelListing.Application.Interfaces;
using HotelListing.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace HotelListing.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class HotelsController : Controller
    {
        private readonly IHotelApplication _hotelApplication;

        public HotelsController(IHotelApplication hotelApplication, IMapper mapper)
        {
            _hotelApplication = hotelApplication;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetHotelDto>>> GetAll()
        {
            var hotels = await _hotelApplication.GetAllAsync();
            return Ok(hotels);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetHotelDto>> Get(int id)
        {
            var hotel = await _hotelApplication.Get(id);
            return Ok(hotel);
        }
        [EnableQuery]
        [HttpGet("paged")]
        public async Task<ActionResult<PagedResult<GetHotelDto>>> GetAllByPage([FromQuery] PaginationParameters paginationParameters)
        {
            var hotels = await _hotelApplication.GetAllByPageAsync(paginationParameters);
            return hotels;
        }

        [HttpPost]
        public async Task<ActionResult<CreateHotelDto>> Create([FromBody] CreateHotelDto createHotelDto)
        {
            await _hotelApplication.CreateAsync(createHotelDto);
            return Created("Hotel Created", createHotelDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateHotelDto updateHotelDto)
        {
            await _hotelApplication.UpdateAsync(id, updateHotelDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delele(int id)
        {
            await _hotelApplication.DeleteAsync(id);
            return NoContent();
        }



    }
}
