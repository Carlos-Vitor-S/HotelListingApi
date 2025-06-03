namespace HotelListing.Infra.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task Create(T element);
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
    }
}
