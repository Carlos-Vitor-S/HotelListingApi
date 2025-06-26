using HotelListing.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.Infra.DataContext.Configurations
{
    class CountryConfig : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Countries");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(c => c.ShortName).HasColumnType("VARCHAR(100)").IsRequired();
            builder.HasMany(c => c.Hotels).WithOne(h => h.Country).HasForeignKey(h => h.CountryId);
        }
    }
}