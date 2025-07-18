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

            var AppGroup = app.MapGroup(BaseRoute).WithTags(ResourceName).WithOpenApi();

            AppGroup.MapGet("/", async (IHotelApplication hotelApplication) =>
            {
                var hotels = await hotelApplication.GetAllAsync();
                return Results.Ok(hotels);
            })
            .WithName("GetAllHotels");

            AppGroup.MapGet("/{id}", async (int id, IHotelApplication hotelApplication) =>
            {
                var hotel = await hotelApplication.Get(id);
                return Results.Ok(hotel);
            })
            .WithName("GetHotel");

            AppGroup.MapGet("/paged", async (int pageNumber, int pageSize, IHotelApplication hotelApplication) =>
            {
                var paginationParameters = new PaginationParameters
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                var countries = await hotelApplication.GetAllByPageAsync(paginationParameters);
                return Results.Ok(countries);
            })
            .WithName("GetAllHotelsByPage");

            AppGroup.MapPost("/", async (CreateHotelDto createHotelDto, IHotelApplication hotelApplication) =>
            {
                await hotelApplication.CreateAsync(createHotelDto);
                return Results.Created("Hotel Created", createHotelDto);
            })
            .RequireAuthorization(new AuthorizeAttribute { Roles = Roles.Administrator })
            .WithName("CreateHotel");

            AppGroup.MapPut("/{id:int}", async (int id, UpdateHotelDto updateHotelDto, IHotelApplication hotelApplication) =>
            {
                await hotelApplication.UpdateAsync(id: id, updateHotelDto: updateHotelDto);
                return Results.Ok(updateHotelDto);
            })
            .WithName("UpdateHotel");


            AppGroup.MapDelete("/{id:int}", async (int id, IHotelApplication hotelApplication) =>
            {
                await hotelApplication.DeleteAsync(id);
                return Results.NoContent();
            })
            .WithName("DeleleHotel");
              
        }
    }
} 
