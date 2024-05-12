using Entities.Concrete;
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

        public IQueryable<Book> GetAllBooks(bool TrackChanges) =>
            FindAll(TrackChanges)
            .OrderBy(b=>b.Id);

        public Book GetOneBookId(int id, bool TrackChanges) =>
            FindByCondition(b => b.Id.Equals(id), TrackChanges)
            .SingleOrDefault();

        public void UpdateOneBook(Book book) => Update(book);
    }
}
