namespace HotelListing.Domain.Exceptions
{
    public class ConflictCustomException : Exception
    {
        public ConflictCustomException(string key, string name) : base($"{name} {key} was conflicted")
        {

        }
    }
}
