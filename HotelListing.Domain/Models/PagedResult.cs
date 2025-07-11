using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Domain.Models
{
    public class PagedResult<T>
    {
        public int TotalItems { get; set; }     
        public int CurrentPage { get; set; }     
        public int PageSize { get; set; }        
        public IEnumerable<T> Items { get; set; }
    }
}
