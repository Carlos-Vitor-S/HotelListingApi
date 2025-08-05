using Carter;
using HotelListing.Application.Applications;
using HotelListing.Application.Interfaces;
using HotelListing.Application.Mappings;
using HotelListing.Domain.Interfaces.IRepositories;
using HotelListing.Domain.Interfaces.IServices;
using HotelListing.Domain.Services;
using HotelListing.Infra.DataContext;
using HotelListing.Infra.Repository;
using HotelListing.Shared.Extensions;
using HotelListing.Shared.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDataProtection();
builder.Services.AddCarter();
builder.Services.AddDbContext<HotelListingContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

// Authentication e Authorization
builder.Services.AddJwtAuthService(builder.Configuration);
builder.Services.AddAuthorization();

// Identity
builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<HotelListingContext>();

// Repositories / Services / Applications
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IUserManagerRepository, UserManagerRepository>();

builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddScoped<IUserManagerService, UserManagerService>();

builder.Services.AddScoped<ICountryApplication, CountryApplication>();
builder.Services.AddScoped<IHotelApplication, HotelApplication>();
builder.Services.AddScoped<IUserManagerApplication, UserManagerApplication>();

builder.Services.AddAutoMapper(typeof(CountryProfile), typeof(HotelProfile), typeof(UserProfile));

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "allowOnlyLocalhostAccess", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
         .AllowAnyHeader()
         .AllowAnyMethod();
    });

});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseCors("allowOnlyLocalhostAccess");

app.UseMiddleware<ExceptionHandlerCustomMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapCarter();
app.Run();
