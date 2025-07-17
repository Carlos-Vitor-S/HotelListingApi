using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Domain.Exceptions
{
    public class UnauthorizedAccessCustomException : Exception
    {
        public UnauthorizedAccessCustomException(string key, string name) : base($"{name} {key} not authorized")
        {

        }
    }
}
