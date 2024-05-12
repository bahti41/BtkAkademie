using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IBookRepository:IRepositoryBase<Book>
    {
        IQueryable<Book> GetAllBooks(bool TrackChanges);
        Book GetOneBookId(int id,bool TrackChanges);
        void CreateOneBook(Book book);
        void UpdateOneBook(Book book);
        void DeleteOneBook(Book book);
    }
}
