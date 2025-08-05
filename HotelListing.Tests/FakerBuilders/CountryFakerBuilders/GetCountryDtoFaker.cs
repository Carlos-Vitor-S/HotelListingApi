using Bogus;
using HotelListing.Application.DTOs.CountryDTOs;

namespace HotelListing.Tests.FakerBuilders.CountryFakerBuilders
{
    public class GetCountryDtoFaker
    {
        private static readonly Faker<GetCountryDto> _getCountryDto = new Faker<GetCountryDto>()
           .RuleFor(getCountryDto => getCountryDto.Id, faker => faker.Random.Int(1, 10))
           .RuleFor(getCountryDto => getCountryDto.Name, fakerGetCountryDto => fakerGetCountryDto.Address.Country())
           .RuleFor(getCountryDto => getCountryDto.ShortName, fakerGetCountryDto => fakerGetCountryDto.Address.CountryCode());

        public static GetCountryDto Generate()
        {
            var countryFaker = _getCountryDto.Generate();
            return countryFaker;
        }
    }
}
