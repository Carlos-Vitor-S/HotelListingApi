using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Domain.Exceptions
{
    public class NotFoundExceptionCustom : Exception
    {
        public NotFoundExceptionCustom(string key , string name) : base($"{name} {key} was not found")
        {
            
        }
    }
}
