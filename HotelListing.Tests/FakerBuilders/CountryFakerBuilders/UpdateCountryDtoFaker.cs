using Bogus;
using HotelListing.Application.DTOs.CountryDTOs;

namespace HotelListing.Tests.FakerBuilders.CountryFakerBuilders
{
    public static class UpdateCountryDtoFaker
    {
        private static readonly Faker<UpdateCountryDto> _updateCountryDto = new Faker<UpdateCountryDto>()
            .RuleFor(updateCountryDto => updateCountryDto.Name, fakerUpdateCountryDto => fakerUpdateCountryDto.Address.Country())
            .RuleFor(updateCountryDto => updateCountryDto.ShortName, fakerUpdateCountryDto => fakerUpdateCountryDto.Address.CountryCode());

        public static UpdateCountryDto Generate()
        {
            var countryFaker = _updateCountryDto.Generate();
            return countryFaker;
        }

    }
}
