namespace HotelListing.Domain.Exceptions
{
    public class NotFoundCustomException : Exception
    {
        public NotFoundCustomException(string key, string name) : base($"{name} {key} was not found")
        {

        }
    }
}
