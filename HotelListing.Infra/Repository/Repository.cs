using HotelListing.Infra.DataContext;
using HotelListing.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Infra.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly HotelListingContext _context;

        public Repository(HotelListingContext context)
        {
            _context = context;
        }

        public async Task Create(T element)
        {
            await _context.Set<T>().AddAsync(element);
        }

        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }
    }
}
