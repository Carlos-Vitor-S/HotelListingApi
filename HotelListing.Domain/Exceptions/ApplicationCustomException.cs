namespace HotelListing.Domain.Exceptions
{
    public class ApplicationCustomException : Exception
    {
        public ApplicationCustomException(string key, string name) : base($"{name} {key} data problem")
        {

        }
    }
}
