using Carter;
using HotelListing.Application.DTOs.CountryDTOs;
using HotelListing.Application.Interfaces;
using HotelListing.Application.Models;

namespace HotelListing.ApiMinimal.Modules
{
    public class CountriesModules : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            const string ResourceName = "Countries";
            const string BaseRoute = $"/minimalApi/{ResourceName}";
            const string RouteWithId = $"{BaseRoute}/{{id:int}}";

            app.MapGet(BaseRoute, async (ICountryApplication countryApplication) =>
            {
                var countries = await countryApplication.GetAllAsync();
                return Results.Ok(countries);
            })
            .WithName("GetAllCountries")
            .WithTags(ResourceName)
            .WithOpenApi();

            app.MapGet(RouteWithId, async (int id, ICountryApplication countryApplication) =>
            {
                var country = await countryApplication.GetDetails(id);
                return Results.Ok(country);
            })
            .WithName("GetCountryDetails")
            .WithTags(ResourceName)
            .WithOpenApi();
        
            app.MapGet($"{BaseRoute}/paged", async (int PageNumber, int PageSize , ICountryApplication countryApplication) =>
            {
                var paginationParameters = new PaginationParameters
                {
                    PageNumber = PageNumber,
                    PageSize = PageSize
                };
               
                var pagedCountries = await countryApplication.GetAllByPageAsync(paginationParameters);
                return Results.Ok(pagedCountries);
            })
            .WithName("GetAllCountriesByPage")
            .WithTags(ResourceName)
            .WithOpenApi();

            app.MapPost(BaseRoute, async (CreateCountryDto createCountryDto, ICountryApplication countryApplication) =>
            {
                await countryApplication.CreateAsync(createCountryDto);
                return Results.Created("Country Created", createCountryDto);
            })
            .WithName("CreateCountry")
            .WithTags(ResourceName)
            .WithOpenApi();

            app.MapPut(RouteWithId, async (int id, UpdateCountryDto updateCountryDto, ICountryApplication countryApplication) =>
            {
                await countryApplication.UpdateAsync(id: id, updateCountryDto: updateCountryDto);
                return Results.Ok(updateCountryDto);
            })
            .WithName("UpdateCountry")
            .WithTags(ResourceName)
            .WithOpenApi();

            app.MapDelete(RouteWithId, async (int id, ICountryApplication countryApplication) =>
            {
                await countryApplication.DeleteAsync(id);
                return Results.NoContent();
            })
            .WithName("DeleteCountry")
            .WithTags(ResourceName)
            .WithOpenApi();
        }
    }
}
