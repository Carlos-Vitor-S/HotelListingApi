using AutoMapper;
using HotelListing.Application.DTOs.HotelDTOs;
using HotelListing.Application.Interfaces;
using HotelListing.Domain.Exceptions;
using HotelListing.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class HotelsController : Controller
    {
        private readonly IHotelApplication _hotelApplication;
        private readonly IMapper _mapper;

        public HotelsController(IHotelApplication hotelApplication, IMapper mapper)
        {
            _hotelApplication = hotelApplication;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetHotelDto>>> GetAll()
        {
            var hotels = await _hotelApplication.GetAllAsync();
            var hotelsDto = _mapper.Map<List<GetHotelDto>>(hotels);
            return Ok(hotelsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetHotelDto>> Get(int id)
        {
            var hotel = await _hotelApplication.Get(id);
            var hotelDto = _mapper.Map<GetHotelDto>(hotel);
            return Ok(hotelDto);
        }

        [HttpPost]
        public async Task<ActionResult<CreateHotelDto>> Create([FromBody] CreateHotelDto createHotelDto)
        {
            var hotel = _mapper.Map<Hotel>(createHotelDto);
            await _hotelApplication.CreateAsync(hotel);
            var hotelDto = _mapper.Map<GetHotelDto>(hotel);
            return CreatedAtAction(nameof(Get), new { id = hotelDto.Id }, hotelDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateHotelDto updateHotelDto)
        {
            if (id != updateHotelDto.Id) return BadRequest();

            var hotel = _mapper.Map<Hotel>(updateHotelDto);
            await _hotelApplication.UpdateAsync(hotel);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delele(int id, [FromBody] UpdateHotelDto updateHotelDto)
        {
            if (id != updateHotelDto.Id) return BadRequest();

            var hotel = _mapper.Map<Hotel>(updateHotelDto);
            await _hotelApplication.UpdateAsync(hotel);
            return NoContent();
        }

    }
}
