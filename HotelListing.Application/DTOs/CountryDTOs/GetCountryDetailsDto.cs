using HotelListing.Application.DTOs.HotelDTOs;

namespace HotelListing.Application.DTOs.CountryDTOs
{
    public class GetCountryDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public List<GetHotelDto> Hotels { get; set; }
    }
}
