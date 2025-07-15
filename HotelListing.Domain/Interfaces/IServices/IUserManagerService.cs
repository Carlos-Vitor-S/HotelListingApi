using HotelListing.Domain.Models;

namespace HotelListing.Domain.Interfaces.IServices
{
    public interface IUserManagerService
    {
        Task<AuthResponse> LoginAsync(User user);
        Task<AuthResponse> RegisterAsync(User user , string role);
    }
}
