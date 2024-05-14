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

        public async Task<BookDTO> CreatOneBookAsync(BookForInsertionDTO
            bookDto)
        {
            var entity = _mapper.Map<Book>(bookDto);
            _manager.Book.CreateOneBook(entity);

            await _manager.SaveAsync();
            return _mapper.Map<BookDTO>(entity);
        }


        public async Task DeleteOneBookAsync(int id, bool trachChanges)
        {
            var entity = await GetOneBookByIdAndcheckExits(id, trachChanges);

            _manager.Book.DeleteOneBook(entity);
            await _manager.SaveAsync();
        }


        public async Task<IEnumerable<BookDTO>> GetAllBooksAsync(bool trackChanges)
        {
            var books = await _manager.Book.GetAllBooksAsync(trackChanges);

            return _mapper.Map<IEnumerable<BookDTO>>(books);
        }


        public async Task<BookDTO> GetOneBookIdAsync(int id, bool trachChanges)
        {
            var book = await GetOneBookByIdAndcheckExits(id, trachChanges);

            return _mapper.Map<BookDTO>(book);
        }


        public async Task<(BookForUpdateDTO bookForUpdateDto, Book book)> GetOneBookForPatchAsync(int id, bool trachChanges)
        {
            var book = await GetOneBookByIdAndcheckExits(id, trachChanges);

            var bookForUpdateDto = _mapper.Map<BookForUpdateDTO>(book);

            return (bookForUpdateDto, book);
        }


        public async Task SaveChangesForPatchAsync(BookForUpdateDTO bookForUpdateDTO, Book book)
        {
            _mapper.Map(bookForUpdateDTO, book);

            await _manager.SaveAsync();
        }


        public async Task UpdateOneBookAsync(int id, BookForUpdateDTO bookDto, bool trachChanges)
        {
            var entity = await GetOneBookByIdAndcheckExits(id, trachChanges);

            entity = _mapper.Map<Book>(bookDto);

            _manager.Book.Update(entity);
            await _manager.SaveAsync();
        }




        // Private Methodlar(BussinesMethod)

        private async Task<Book> GetOneBookByIdAndcheckExits(int id, bool trachChanges)
        {
            var entity = await _manager.Book.GetOneBookIdAsync(id, trachChanges);

            if (entity is null)
                throw new BookNotFoundException(id);
            
            return entity;
        }
    }
}
