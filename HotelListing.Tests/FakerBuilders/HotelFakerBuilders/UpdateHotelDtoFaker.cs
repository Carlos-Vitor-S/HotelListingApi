using Bogus;
using HotelListing.Application.DTOs.HotelDTOs;

namespace HotelListing.Tests.FakerBuilders.HotelFakerBuilders
{
    internal class UpdateHotelDtoFaker
    {
        private static readonly Faker<UpdateHotelDto> _updateHotelDtoFaker = new Faker<UpdateHotelDto>()
          .RuleFor(createHotelDto => createHotelDto.Name, createHotelDtoFaker => createHotelDtoFaker.Company.CompanyName())
          .RuleFor(createHotelDto => createHotelDto.Address, createHotelDtoFaker => createHotelDtoFaker.Address.FullAddress())
          .RuleFor(createHotelDto => createHotelDto.Rating, createHotelDtoFaker => Math.Round(createHotelDtoFaker.Random.Double(0, 5), 1))
          .RuleFor(createHotelDto => createHotelDto.CountryId, createHotelDtoFaker => createHotelDtoFaker.Random.Int(1, 250));
        public static UpdateHotelDto Generate()
        {
            var hotelFaker = _updateHotelDtoFaker.Generate();
            return hotelFaker;
        }
    }
}
