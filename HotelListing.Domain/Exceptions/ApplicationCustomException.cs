using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Domain.Exceptions
{
    public class ApplicationCustomException : Exception
    {
        public ApplicationCustomException(string key, string name) : base($"{name} {key} data problem")
        {

        }
    }
}
