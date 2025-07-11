using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Domain.Models
{
    public class PaginationParameters
    {
        private int _pageSize = 3;

        public int PageNumber { get; set; }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value;
        }
    }
}
