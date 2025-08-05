using Carter;
using HotelListing.Application.DTOs.ApiAuthDTOs;
using HotelListing.Application.DTOs.UserDTOs;
using HotelListing.Application.Interfaces;

namespace HotelListing.ApiMinimal.Modules
{
    public class UserManagerModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            const string ResourceName = "UserManager";
            const string BaseRoute = "/minimalApi";

            var AppGroup = app.MapGroup(BaseRoute).WithTags(ResourceName).WithOpenApi();

            AppGroup.MapPost("/Register", async (RegisterUserDto registerUserDto, string role, IUserManagerApplication userManagerApplication) =>
            {
                var authResponse = await userManagerApplication.RegisterAsync(registerUserDto, role: role);
                return Results.Ok(authResponse);
            })
            .WithName("Register");

            AppGroup.MapPost("/Login", async (LoginUserDto loginUserDto, IUserManagerApplication userManagerApplication) =>
            {
                var authResponse = await userManagerApplication.LoginAsync(loginUserDto);
                return Results.Ok(authResponse);
            })
            .WithName("Login");
        }
    }
}
