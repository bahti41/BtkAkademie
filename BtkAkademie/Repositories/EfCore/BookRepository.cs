using Entities.Concrete;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EfCore
{
    public sealed class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(BookDbContext context) : base(context)
        {

        }

        public void CreateOneBook(Book book) => Create(book);

        public void DeleteOneBook(Book book) => Delete(book);

        public async Task<PagedList<Book>> GetAllBooksAsync(BookParameters bookParameters, bool TrackChanges)
        {
            var books = await FindAll(TrackChanges)
                .FilterBooks(bookParameters.MinPrice, bookParameters.MaxPrice)
                .Search(bookParameters.SearchTrem)
                .Sort(bookParameters.OrderBy)
                .ToListAsync();

            return PagedList<Book>.ToPagedList(books, bookParameters.PageNumber, bookParameters.PageSize);
        }

        public async Task<Book> GetOneBookIdAsync(int id, bool TrackChanges) =>
            await FindByCondition(b => b.Id.Equals(id), TrackChanges)
            .SingleOrDefaultAsync();

        public void UpdateOneBook(Book book) => Update(book);

        //Verioning
        public async Task<List<Book>> GetAllBooksAsync(bool trachChanges)
        {
            return await FindAll(trachChanges).OrderBy(b=> b.Id).ToListAsync();
        }
    }
}
