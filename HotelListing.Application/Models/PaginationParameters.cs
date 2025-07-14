namespace HotelListing.Application.Models
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
