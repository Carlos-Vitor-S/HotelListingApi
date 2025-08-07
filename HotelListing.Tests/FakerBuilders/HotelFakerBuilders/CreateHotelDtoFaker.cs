using Bogus;
using HotelListing.Application.DTOs.HotelDTOs;

namespace HotelListing.Tests.FakerBuilders.HotelFakerBuilders
{
    public class CreateHotelDtoFaker
    {
        private static readonly Faker<CreateHotelDto> _createHotelDtoFaker = new Faker<CreateHotelDto>()
            .RuleFor(createHotelDto => createHotelDto.Name, createHotelDtoFaker => createHotelDtoFaker.Company.CompanyName())
            .RuleFor(createHotelDto => createHotelDto.Address, createHotelDtoFaker => createHotelDtoFaker.Address.FullAddress())
            .RuleFor(createHotelDto => createHotelDto.Rating, createHotelDtoFaker => Math.Round(createHotelDtoFaker.Random.Double(0, 5), 1))
            .RuleFor(createHotelDto => createHotelDto.CountryId, createHotelDtoFaker => createHotelDtoFaker.Random.Int(1, 250));
        public static CreateHotelDto Generate()
        {
            var hotelFaker = _createHotelDtoFaker.Generate();
            return hotelFaker;
        }
    }
}
