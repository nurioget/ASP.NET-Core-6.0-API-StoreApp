namespace Entities.Exceptions
{
    public sealed class BookNotFoundException : NotFoundException
    {
        public BookNotFoundException(int id) 
            : base($"The Book With id : {id} could not found.")
        {

        }
    }
}
