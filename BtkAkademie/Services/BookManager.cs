using Entities.Concrete;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;

        public BookManager(IRepositoryManager manager, ILoggerService logger)
        {
            _manager = manager;
            _logger = logger;
        }

        public Book CreatOneBook(Book book)
        {
            _manager.Book.CreateOneBook(book);
            _manager.Save();
            return book;
        }


        public void DeleteOneBook(int id, bool trachChanges)
        {
            var entity = _manager.Book.GetOneBookId(id, trachChanges);
            if (entity is null)
            {
                string message = $"The book with if:{id} could not found";
                _logger.LogInfo(message);
                throw new Exception(message);
            }

            _manager.Book.DeleteOneBook(entity);
            _manager.Save();
        }


        public IEnumerable<Book> GetAllBooks(bool trackChanges)
        {
            return _manager.Book.GetAllBooks(trackChanges);
        }


        public Book GetOneBookId(int id, bool trachChanges)
        {
            return _manager.Book.GetOneBookId(id, trachChanges);
        }


        public void UpdateOneBook(int id, Book book, bool trachChanges)
        {
            var entity = _manager.Book.GetOneBookId(id, trachChanges);
            if (entity is null)
            {
                string message = $"Book with id:{id} could not found.";
                _logger.LogInfo(message);
                throw new Exception(message);
            }
                


            if (book is null)
                throw new ArgumentNullException(nameof (book));

            entity.Title= book.Title;
            entity.Price= book.Price;

            _manager.Book.Update(entity);
            _manager.Save();
        }
    }
}
