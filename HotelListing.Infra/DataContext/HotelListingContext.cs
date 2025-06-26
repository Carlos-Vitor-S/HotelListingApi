using HotelListing.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Infra.DataContext
{
    public class HotelListingContext : IdentityDbContext<IdentityUser>
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
