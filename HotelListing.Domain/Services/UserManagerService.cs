
using HotelListing.Domain.Interfaces.IRepositories;
using HotelListing.Domain.Interfaces.IServices;
using HotelListing.Domain.Models;

namespace HotelListing.Domain.Services
{
    public class UserManagerService : IUserManagerService
    {
        private readonly IUserManagerRepository _userManagerRepository;

        public UserManagerService(IUserManagerRepository userManagerRepository)
        {
            _userManagerRepository = userManagerRepository;
        }

        public async Task<AuthResponse> LoginAsync(User user)
        {
            var identityUser = await _userManagerRepository.FindByEmailAsync(user.Email);

            if (identityUser == null || !await _userManagerRepository.CheckPasswordAsync(identityUser, user.Password))
            {
                throw new UnauthorizedAccessException("Credenciais inválidas.");
            }

            var token = await _userManagerRepository.GenerateTokenAsync(identityUser);

            return new AuthResponse
            {
                UserId = identityUser.Id,
                Token = token
            };

        }

        public async Task<AuthResponse> RegisterAsync(User user , string role)
        {
            var result = await _userManagerRepository.Register(user , role);

            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new ApplicationException($"Registro falhou: {errors}");
            }

            var identityUser = await _userManagerRepository.FindByEmailAsync(user.Email);

            var token = await _userManagerRepository.GenerateTokenAsync(identityUser);

            return new AuthResponse
            {
                UserId = identityUser.Id,
                Token = token
            };
        }
    }
}
