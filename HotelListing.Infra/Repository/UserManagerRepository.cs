using HotelListing.Domain.Interfaces.IRepositories;
using HotelListing.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace HotelListing.Infra.Repository
{
    public class UserManagerRepository : IUserManagerRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;


        public UserManagerRepository(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<AuthResponse> Login(User user)
        {
            var identityUser = await _userManager.FindByEmailAsync(user.Email);
            var isValidPassword = await _userManager.CheckPasswordAsync(identityUser, user.Password);

            var token = await GenerateTokenAsync(identityUser);

            return new AuthResponse
            {
                Token = token,
                UserId = identityUser.Id,
            };
        }

        public async Task<IdentityResult> Register(User user , string role)
        {
            var identityUser = new IdentityUser
            {
                UserName = user.UserName,
                Email = user.Email,
            };

            var result = await _userManager.CreateAsync(identityUser, user.Password);

            if (result.Succeeded) { 
                await _userManager.AddToRoleAsync(identityUser, role);
            }
          
            return result;
        }

        public async Task<string> GenerateTokenAsync(IdentityUser identityUser)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(identityUser);
            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r)).ToList();
            var userClaims = await _userManager.GetClaimsAsync(identityUser);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, identityUser.Email),
                new Claim(JwtRegisteredClaimNames.Email, identityUser.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Uid", identityUser.Id),
            }
            .Union(userClaims)
            .Union(roleClaims);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtSettings:DurationInMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public Task<bool> CheckPasswordAsync(IdentityUser identityUser, string password)
        {
            return _userManager.CheckPasswordAsync(identityUser, password);
        }

        public Task<IList<string>> GetRolesAsync(IdentityUser identityUser)
        {
            return _userManager.GetRolesAsync(identityUser);
        }

        public Task<IList<Claim>> GetClaimsAsync(IdentityUser identityUser)
        {
            return _userManager.GetClaimsAsync(identityUser);
        }

        public async Task<IdentityUser> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

    }
}
