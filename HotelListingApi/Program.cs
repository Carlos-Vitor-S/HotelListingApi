using HotelListing.Application.Applications;
using HotelListing.Application.Interfaces;
using HotelListing.Domain.Services;
using HotelListing.Infra.DataContext;
using HotelListing.Infra.Interfaces;
using HotelListing.Infra.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<HotelListingContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddOpenApi();

builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<ICountryApplication, CountryApplication>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddControllers()
    .AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
