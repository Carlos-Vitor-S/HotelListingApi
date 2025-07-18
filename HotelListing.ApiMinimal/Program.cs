
using Carter;
using HotelListing.Application.Applications;
using HotelListing.Application.Interfaces;
using HotelListing.Application.Mappings;
using HotelListing.Domain.Interfaces.IRepositories;
using HotelListing.Domain.Interfaces.IServices;
using HotelListing.Domain.Services;
using HotelListing.Infra.DataContext;
using HotelListing.Infra.Repository;
using HotelListing.Shared.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Serviços principais
builder.Services.AddDataProtection();
builder.Services.AddCarter();
builder.Services.AddDbContext<HotelListingContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

// Authentication e Authorization
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            builder.Configuration["JwtSettings:Key"]
            ))
    };
});
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
    options.AddPolicy(name: "frontEndPolicy", policy =>
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


app.UseCors("frontEndPolicy");

app.UseMiddleware<ExceptionHandlerCustomMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapCarter();
app.Run();
