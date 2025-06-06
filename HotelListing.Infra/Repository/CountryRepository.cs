using HotelListing.Domain.Interfaces;
using HotelListing.Domain.Models;
using HotelListing.Infra.DataContext;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Infra.Repository
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        private readonly HotelListingContext _context;
        public CountryRepository(HotelListingContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Country> GetDetails(int id)
        {
            return await _context.Countries.Include(c => c.Hotels).FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
