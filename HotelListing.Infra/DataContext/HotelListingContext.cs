using HotelListing.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Infra.DataContext
{
    public class HotelListingContext : DbContext
    {
        public HotelListingContext(DbContextOptions<HotelListingContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HotelListingContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
    }
}
