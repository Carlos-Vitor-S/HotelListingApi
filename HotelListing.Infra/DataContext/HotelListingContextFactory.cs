using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HotelListing.Infra.DataContext
{
    public class HotelListingContextFactory : IDesignTimeDbContextFactory<HotelListingContext>
    {
        public HotelListingContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HotelListingContext>();

            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=HotelListingDatabase;User Id=postgres;Password=postzeus2011;");

            return new HotelListingContext(optionsBuilder.Options);
        }
    }
}
