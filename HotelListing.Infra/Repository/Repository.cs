using HotelListing.Domain.Interfaces.IRepositories;
using HotelListing.Infra.DataContext;
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

        public async Task CreateAsync(T element)
        {
            await _context.Set<T>().AddAsync(element);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var element = await _context.Set<T>().FindAsync(id);
            _context.Set<T>().Remove(element);
            await SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public IQueryable<T> GetAllAsQueryable()
        {
            return _context.Set<T>().AsQueryable();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T element)
        {
            _context.Set<T>().Update(element);
            await SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var element = await GetAsync(id);
            return element != null;
        }

        private async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
