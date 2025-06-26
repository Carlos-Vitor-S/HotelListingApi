namespace HotelListing.Domain.Exceptions
{
    public class HotelException : Exception
    {
        public HotelException(string message)
            : base($"Hotel Exception : {message}")
        {
        }
    }
}
