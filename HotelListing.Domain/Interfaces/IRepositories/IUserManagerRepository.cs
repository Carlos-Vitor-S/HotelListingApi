using HotelListing.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
namespace HotelListing.Domain.Interfaces.IRepositories
{
    public interface IUserManagerRepository
    {
        Task<IdentityResult> Register(User user);
        Task<AuthResponse> Login(User user);
        Task<string> GenerateTokenAsync(IdentityUser identityUser);
        Task<bool> CheckPasswordAsync(IdentityUser identityUser , string password);
        Task<IList<string>> GetRolesAsync(IdentityUser identityUser);
        Task<IList<Claim>> GetClaimsAsync(IdentityUser identityUser);
        Task<IdentityUser> FindByEmailAsync(string email);
    }
}

