﻿using AutoMapper;
using Entities.Concrete;
using Entities.DTOs;
using Entities.Exceptions;
using Entities.Exceptions.Category;
using Entities.LinkModels;
using Entities.RequestFeatures;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookManager : IBookService
    {
        private readonly ICategoryService _categoryService;
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly IBookLinks _bookLinks;

        public BookManager(IRepositoryManager manager,
            ILoggerService logger,
            IMapper mapper,
            IBookLinks bookLinks,
            ICategoryService categoryService)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
            _bookLinks = bookLinks;
            _categoryService = categoryService;
        }

        public async Task<BookDTO> CreatOneBookAsync(BookForInsertionDTO
            bookDto)
        {
            var category = await _categoryService.GetOneCategoryByIdAsync(bookDto.CategoryId, false);

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


        public async Task<(LinkResponse linkResponse, MetaData metaData)> GetAllBooksAsync(LinkParameters linkParameters, bool trackChanges)
        {
            if (!linkParameters.BookParameters.ValidPriceRange)
                throw new PriceOutofRangeBadRequestException();

            var booksWithMetaData = await _manager.Book.GetAllBooksAsync(linkParameters.BookParameters,trackChanges);

            var bookDto = _mapper.Map<IEnumerable<BookDTO>>(booksWithMetaData);

            var links = _bookLinks.TryGenerateLinks(bookDto, linkParameters.BookParameters.Fields, linkParameters.HttpContext);

            return (links, booksWithMetaData.MetaData);
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

        // Verioning
        public Task<List<Book>> GetAllBooksAsync(bool trachChanges)
        {
            var books = _manager.Book.GetAllBooksAsync(trachChanges);
            return books;
        }

        //İlişkisel Durum icin
        public async Task<IEnumerable<Book>> GetAllBooksWithDetailsAsync(bool trachChanges)
        {
            return await _manager.Book.GetAllBooksWithDetailsAsync(trachChanges);
        }
    }
}
