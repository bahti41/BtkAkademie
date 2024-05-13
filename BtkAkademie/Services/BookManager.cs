using AutoMapper;
using Entities.Concrete;
using Entities.DTOs;
using Entities.Exceptions;
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
        private readonly IMapper _mapper;

        public BookManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }

        public BookDTO CreatOneBook(BookForInsertionDTO
            bookDto)
        {
            var entity = _mapper.Map<Book>(bookDto);
            _manager.Book.CreateOneBook(entity);
            _manager.Save();
            return _mapper.Map<BookDTO>(entity);
        }


        public void DeleteOneBook(int id, bool trachChanges)
        {
            var entity = _manager.Book.GetOneBookId(id, trachChanges);
            if (entity is null)
                throw new BookNotFoundException(id);

            _manager.Book.DeleteOneBook(entity);
            _manager.Save();
        }


        public IEnumerable<BookDTO> GetAllBooks(bool trackChanges)
        {
            var books = _manager.Book.GetAllBooks(trackChanges);
            return _mapper.Map<IEnumerable<BookDTO>>(books);
        }

        public (BookForUpdateDTO bookForUpdateDTO, Book book) GetOneBookForPatch(int id, bool trachChanges)
        {
            var book = _manager.Book.GetOneBookId(id,trachChanges);

            if(book is null)
                throw new BookNotFoundException(id);

            var bookForUpdateDto = _mapper.Map<BookForUpdateDTO>(book);

            return (bookForUpdateDto,book);


        }

        public BookDTO GetOneBookId(int id, bool trachChanges)
        {
            var book = _manager.Book.GetOneBookId(id, trachChanges);
            if (book is null)
                throw new BookNotFoundException(id);
            return _mapper.Map<BookDTO>(book);
        }

        public void SaveChangesForPatch(BookForUpdateDTO bookForUpdateDTO, Book book)
        {
            _mapper.Map(bookForUpdateDTO, book);
            _manager.Save();
        }

        public void UpdateOneBook(int id, BookForUpdateDTO bookDto, bool trachChanges)
        {
            var entity = _manager.Book.GetOneBookId(id, trachChanges);
            if (entity is null)
                throw new BookNotFoundException(id);

            entity = _mapper.Map<Book>(bookDto);

            _manager.Book.Update(entity);
            _manager.Save();
        }
    }
}
