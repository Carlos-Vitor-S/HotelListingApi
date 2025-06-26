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
            try
            {
                var hotels = await _hotelApplication.GetAllAsync();
                var hotelsDto = _mapper.Map<List<GetHotelDto>>(hotels);
                return Ok(hotelsDto);
            }
            catch (HotelException hotelException)
            {
                {
                    return NotFound(hotelException.Message);
                }
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetHotelDto>> Get(int id)
        {
            try
            {
                var hotel = await _hotelApplication.Get(id);
                var hotelDto = _mapper.Map<GetHotelDto>(hotel);
                return Ok(hotelDto);
            }
            catch (HotelException hoteException)
            {
                return NotFound(hoteException.Message);
            }
        }

        [HttpPost]

        public async Task<ActionResult<CreateHotelDto>> Create([FromBody] CreateHotelDto createHotelDto)
        {
            try
            {
                var hotel = _mapper.Map<Hotel>(createHotelDto);
                await _hotelApplication.CreateAsync(hotel);
                var hotelDto = _mapper.Map<GetHotelDto>(hotel);
                return CreatedAtAction(nameof(Get), new { id = hotelDto.Id }, hotelDto);

            }
            catch (HotelException hotelException)
            {
                return Conflict(hotelException.Message);
            }
        }
    }

}
