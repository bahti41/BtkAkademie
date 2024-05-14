using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EfCore
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(BookDbContext context) : base(context)
        {

        }

        public void CreateOneBook(Book book) => Create(book);

        public void DeleteOneBook(Book book) => Delete(book);

        public async Task<IEnumerable<Book>> GetAllBooksAsync(bool TrackChanges) =>
            await FindAll(TrackChanges)
            .OrderBy(b=>b.Id)
            .ToListAsync();

        public async Task<Book> GetOneBookIdAsync(int id, bool TrackChanges) =>
            await FindByCondition(b => b.Id.Equals(id), TrackChanges)
            .SingleOrDefaultAsync();

        public void UpdateOneBook(Book book) => Update(book);
    }
}
