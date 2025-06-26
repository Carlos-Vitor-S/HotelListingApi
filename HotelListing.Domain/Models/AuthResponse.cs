using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Domain.Models
{
    public class AuthResponse
    {
        public string UserId { get; set; }
        public string Token { get; set; }
    }
}
