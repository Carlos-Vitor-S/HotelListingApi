using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HotelListing.Infra.DataContext
{
    public class HotelListingContextFactory : IDesignTimeDbContextFactory<HotelListingContext>
    {
        public HotelListingContext CreateDbContext(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<HotelListingContext>();
            var connectionString = config.GetConnectionString("DefaultConnection");

            optionsBuilder.UseNpgsql(connectionString);

            return new HotelListingContext(optionsBuilder.Options);
        }
    }
}
