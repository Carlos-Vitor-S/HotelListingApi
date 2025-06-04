using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Domain.Exceptions.CountryExceptions
{
    public class CountryException : Exception
    {
        public CountryException(string message)
            : base($"{message}")
        {
        }
    }

}
