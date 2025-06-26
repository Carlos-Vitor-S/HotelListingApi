namespace HotelListing.Domain.Exceptions
{
    public class CountryException : Exception
    {
        public CountryException(string message)
            : base($"Country Exception : {message}")
        {
        }
    }

}
