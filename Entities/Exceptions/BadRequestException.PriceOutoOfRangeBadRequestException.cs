namespace Entities.Exceptions
{
    public abstract partial class BadRequestException
    {
        public class PriceOutoOfRangeBadRequestException : BadRequestException
        {
            public PriceOutoOfRangeBadRequestException() : base("Maximum Price should be less than 1000 and greather than 10.")
            {

            }
        }
    }
}
