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
