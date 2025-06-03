namespace HotelListing.Infra.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task Create(T element);
        Task Update(T element);
        Task Delete(int id);
        Task SaveChanges();
    }
}
