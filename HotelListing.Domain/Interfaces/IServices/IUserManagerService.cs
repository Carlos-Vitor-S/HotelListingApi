using HotelListing.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Domain.Interfaces.IServices
{
    public interface IUserManagerService
    {
        Task<AuthResponse> LoginAsync(User user);
        Task<AuthResponse> RegisterAsync(User user);
    }
}
