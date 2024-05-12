namespace Entities.Exceptions
{
    public sealed class BookNotFoundException : NotFoundExcetion
    {
        public BookNotFoundException(int id): base($"the book with id :{id} could not found")
        {
            
        }
    }
}
