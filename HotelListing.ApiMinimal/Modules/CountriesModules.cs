using Carter;
using HotelListing.Application.DTOs.CountryDTOs;
using HotelListing.Application.Interfaces;
using HotelListing.Application.Models;
using HotelListing.Application.Utils;

namespace HotelListing.ApiMinimal.Modules
{
    public class CountriesModules : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            const string ResourceName = "Countries";
            const string BaseRoute = $"/minimalApi/{ResourceName}";

            var AppGroup = app.MapGroup(BaseRoute).WithTags(ResourceName).WithOpenApi();

            AppGroup.MapGet("/", async (ICountryApplication countryApplication) =>
            {
                var countries = await countryApplication.GetAllAsync();
                return Results.Ok(countries);
            })
            .WithName("GetAllCountries");

            AppGroup.MapGet("/{id}", async (int id, ICountryApplication countryApplication) =>
            {
                var country = await countryApplication.GetDetails(id);
                return Results.Ok(country);
            })
            .WithName("GetCountryDetails");

            AppGroup.MapGet("/paged", async (int PageNumber, int PageSize, ICountryApplication countryApplication) =>
            {
                var paginationParameters = new PaginationParameters
                {
                    PageNumber = PageNumber,
                    PageSize = PageSize
                };

                var pagedCountries = await countryApplication.GetAllByPageAsync(paginationParameters);
                return Results.Ok(pagedCountries);
            })
            .WithName("GetAllCountriesByPage");

            AppGroup.MapPost("/", async (CreateCountryDto createCountryDto, ICountryApplication countryApplication) =>
            {
                await countryApplication.CreateAsync(createCountryDto);
                return Results.Created("Country Created", createCountryDto);
            })
            .WithName("CreateCountry")
            .RequireAuthorization(policy => policy.RequireRole(Roles.Administrator, Roles.NormalUser));

            AppGroup.MapPut("/{id}", async (int id, UpdateCountryDto updateCountryDto, ICountryApplication countryApplication) =>
            {
                await countryApplication.UpdateAsync(id: id, updateCountryDto: updateCountryDto);
                return Results.Ok(updateCountryDto);
            })
            .WithName("UpdateCountry");

            AppGroup.MapDelete("/{id}", async (int id, ICountryApplication countryApplication) =>
            {
                await countryApplication.DeleteAsync(id);
                return Results.NoContent();
            })
            .WithName("DeleteCountry")
            .RequireAuthorization(policy => policy.RequireRole(Roles.Administrator));
        }
    }
}
