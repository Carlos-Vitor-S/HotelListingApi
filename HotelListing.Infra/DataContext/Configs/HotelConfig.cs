using HotelListing.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.Infra.DataContext.Configurations
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.ToTable("Hotels");

            builder.HasKey(h => h.Id);
            builder.Property(h => h.Name).HasColumnType("VARCHAR(150)").IsRequired();
            builder.Property(h => h.Address).HasColumnType("VARCHAR(250)").IsRequired();
            builder.Property(h => h.Rating).HasColumnType("double precision").IsRequired();
            builder.Property(h => h.CountryId).IsRequired();
            builder.HasOne(h => h.Country).WithMany(c => c.Hotels).HasForeignKey(h => h.CountryId);
        }
    }
}
