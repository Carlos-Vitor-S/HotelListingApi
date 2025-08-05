namespace HotelListing.Domain.Exceptions
{
    public class UnauthorizedAccessCustomException : Exception
    {
        public UnauthorizedAccessCustomException(string key, string name) : base($"{name} {key} not authorized")
        {

        }
    }
}
