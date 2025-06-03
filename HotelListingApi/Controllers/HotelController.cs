using HotelListing.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class HotelController : ControllerBase
    {
        private static List<Hotel> hotels = new List<Hotel> {
            new Hotel { Id = 1, Name = "Hotel Paraíso", Address = "Rua das Flores, 123 - São Paulo", Rating = 4.5 },
            new Hotel { Id = 2, Name = "Pousada do Sol", Address = "Av. Atlântica, 456 - Rio de Janeiro", Rating = 4.0 },
            new Hotel { Id = 3, Name = "Resort Mar Azul", Address = "Rodovia BR-101, Km 200 - Salvador", Rating = 4.8 },

        };

        [HttpGet]
        public ActionResult<IEnumerable<Hotel>> GetAll()
        {
            return Ok(hotels);
        }

        [HttpGet("{id}")]
        public ActionResult<Hotel> Get(int id)
        {
            var hotelWithRequestedId = hotels.FirstOrDefault(h => h.Id == id);
            if (hotelWithRequestedId == null)
            {
                return NotFound();
            }

            return Ok(hotelWithRequestedId);
        }
        [HttpPost]
        public ActionResult<Hotel> Post([FromBody] Hotel hotel)
        {
            if (hotels.Any(h => h.Id == hotel.Id))
            {
                return BadRequest("Hotel Id ja existe");
            }
            hotels.Add(hotel);
            return CreatedAtAction(nameof(Get), new { id = hotel.Id }, hotel);
        }
        [HttpPut]
        public ActionResult Put([FromBody] Hotel hotel)
        {
            var existingHotel = hotels.FirstOrDefault(h => h.Id == hotel.Id);
            if (existingHotel == null)
            {
                return NotFound("Hotel com id nao encontrado para ser atualizado");
            }

            existingHotel.Name = hotel.Name;
            existingHotel.Address = hotel.Address;
            existingHotel.Rating = hotel.Rating;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var existingHotel = hotels.FirstOrDefault(h => h.Id == id);

            if (existingHotel == null)
            {
                return NotFound("Hotel com id nao encontrado para ser Removido");
            }

            hotels.Remove(existingHotel);
            return NoContent();
        }
    }
}
