using HotelListing.Domain.Interfaces.IRepositories;
using HotelListing.Domain.Models;
using HotelListing.Infra.DataContext;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public async Task<T> Get(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T element)
        {
            _context.Set<T>().Update(element);
            await SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            var element = await Get(id);
            return element != null;
        }

        private async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<T>> GetAllByPageAsync(PaginationParameters paginationParameters)
        {
            var skipCount = (paginationParameters.PageNumber - 1) * paginationParameters.PageSize;

            var totalItems = await _context.Set<T>().CountAsync();
            var items = await _context.Set<T>()
                 .Skip(skipCount)
                 .Take(paginationParameters.PageSize)
                 .ToListAsync();

            return new PagedResult<T>
            {
                Items = items,
                TotalItems = totalItems,
                CurrentPage = paginationParameters.PageNumber,
                PageSize = paginationParameters.PageSize
            };

        }
    }
}
