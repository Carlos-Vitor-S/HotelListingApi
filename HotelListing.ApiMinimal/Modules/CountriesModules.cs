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
            const string ResourceTag = "Countries";
            const string BaseEndpoint = $"/minimalApi/{ResourceTag}";

            app.MapGet(BaseEndpoint, async (ICountryApplication countryApplication) =>
            {
                var countries = await countryApplication.GetAllAsync();
                return Results.Ok(countries);
            })
            .WithName("GetAll")
            .WithTags(ResourceTag)
            .WithOpenApi();

            app.MapGet($"{BaseEndpoint}/{{id:int}}", async (int id, ICountryApplication countryApplication) =>
            {
                var countries = await countryApplication.Get(id);
                return Results.Ok(countries);
            })
            .WithName("Get")
            .WithTags(ResourceTag)
            .WithOpenApi();

            app.MapGet($"{BaseEndpoint}/paged", async (int PageNumber, int PageSize , ICountryApplication countryApplication) =>
            {
                var paginationParameters = new PaginationParameters
                {
                    PageNumber = PageNumber,
                    PageSize = PageSize
                };
               
                var pagedCountries = await countryApplication.GetAllByPageAsync(paginationParameters);
                return Results.Ok(pagedCountries);
            })
            .WithName("GetAllByPage")
            .WithTags(ResourceTag)
            .WithOpenApi();

            app.MapPost(BaseEndpoint, async (CreateCountryDto createCountryDto, ICountryApplication countryApplication) =>
            {
                await countryApplication.CreateAsync(createCountryDto);
                return Results.Created("Country Created", createCountryDto);
            })
            .WithName("Create")
            .WithTags(ResourceTag)
            .WithOpenApi();

            app.MapPut($"{BaseEndpoint}/{{id:int}}", async (int id, UpdateCountryDto updateCountryDto, ICountryApplication countryApplication) =>
            {
                await countryApplication.UpdateAsync(id: id, updateCountryDto: updateCountryDto);
                return Results.Ok(updateCountryDto);
            })
            .WithName("Update")
            .WithTags(ResourceTag)
            .WithOpenApi();

            app.MapDelete($"{BaseEndpoint}/{{id:int}}", async (int id, ICountryApplication countryApplication) =>
            {
                await countryApplication.DeleteAsync(id);
                return Results.NoContent();
            })
            .WithName("Delete")
            .WithTags(ResourceTag)
            .WithOpenApi()
        }
    }
}
