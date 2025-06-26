using System.ComponentModel.DataAnnotations;

namespace HotelListing.Application.DTOs.HotelDTOs
{
    public class CreateHotelDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        public double Rating { get; set; }
        public int CountryId { get; set; }
    }
}
