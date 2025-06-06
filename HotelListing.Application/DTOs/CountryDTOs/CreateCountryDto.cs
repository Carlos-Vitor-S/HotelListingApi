using System.ComponentModel.DataAnnotations;

namespace HotelListing.Application.DTOs.CountryDTOs
{
    public class CreateCountryDto
    {
        [Required]
        public string Name { get; set; }
        public string ShortName { get; set; }

    }
}
