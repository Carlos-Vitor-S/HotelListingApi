using Bogus;
using HotelListing.Application.DTOs.CountryDTOs;

namespace HotelListing.Tests.FakerBuilders.CountryFakerBuilders
{
    public static class CreateCountryDtoFaker
    {
        private static readonly Faker<CreateCountryDto> _createCountryDtoFaker = new Faker<CreateCountryDto>()
            .RuleFor(createCountryDto => createCountryDto.Name, fakeCreateCountryDto => fakeCreateCountryDto.Address.Country())
            .RuleFor(createCountryDto => createCountryDto.ShortName, fakerCreateCountryDto => fakerCreateCountryDto.Address.CountryCode());

        public static CreateCountryDto Generate()
        {
            var countryFaker = _createCountryDtoFaker.Generate();
            return countryFaker;
        }

    }
}
