namespace HotelListing.Domain.Exceptions
{
    public class NotFoundExceptionCustom : Exception
    {
        public NotFoundExceptionCustom(string key, string name) : base($"{name} {key} was not found")
        {

        }
    }
}
