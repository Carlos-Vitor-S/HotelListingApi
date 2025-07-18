using Carter;
using HotelListing.Application.DTOs.HotelDTOs;
using HotelListing.Application.Interfaces;
using HotelListing.Application.Models;
using HotelListing.Application.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HotelListing.ApiMinimal.Modules
{
    public class HotelsModules : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            const string ResourceName = "Hotels";
            const string BaseRoute = $"/minimalApi/{ResourceName}";
            const string RouteWithId = $"{BaseRoute}/{{id:int}}";
       
            app.MapGet(BaseRoute, async (IHotelApplication hotelApplication) =>
            {
                var hotels = await hotelApplication.GetAllAsync();
                return Results.Ok(hotels);
            })
            .WithName("GetAllHotels")
            .WithTags(ResourceName)
            .WithOpenApi();

            app.MapGet(RouteWithId, async (int id , IHotelApplication hotelApplication) =>
            {
                var hotel = await hotelApplication.Get(id);
                return Results.Ok(hotel);
            })
            .WithName("GetHotel")
            .WithTags(ResourceName)
            .WithOpenApi();

            app.MapGet($"{BaseRoute}/paged" , async (int pageNumber , int pageSize , IHotelApplication hotelApplication) =>
            {
                var paginationParameters = new PaginationParameters
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                var countries = await hotelApplication.GetAllByPageAsync(paginationParameters);
                return Results.Ok(countries);
            })
            .WithName("GetAllHotelsByPage")
            .WithTags(ResourceName)
            .WithOpenApi();

            app.MapPost(BaseRoute, async (CreateHotelDto createHotelDto, IHotelApplication hotelApplication) =>
            {
                await hotelApplication.CreateAsync(createHotelDto);
                return Results.Created("Hotel Created", createHotelDto);
            })
            .RequireAuthorization(new AuthorizeAttribute { Roles = Roles.Administrator})
            .WithName("CreateHotel")
            .WithTags(ResourceName)
            .WithOpenApi();
             

            app.MapPut(RouteWithId, async (int id, UpdateHotelDto updateHotelDto, IHotelApplication hotelApplication) =>
            {
                await hotelApplication.UpdateAsync(id: id, updateHotelDto : updateHotelDto);
                return Results.Ok(updateHotelDto);
            })
            .WithName("UpdateHotel")
            .WithTags(ResourceName)
            .WithOpenApi();

            app.MapDelete(RouteWithId, async(int id , IHotelApplication hotelApplication) =>
            {
                await hotelApplication.DeleteAsync(id);
                return Results.NoContent();
            })
            .WithName("DeleleHotel")
            .WithTags(ResourceName)
            .WithOpenApi();
        }
    }
} 
