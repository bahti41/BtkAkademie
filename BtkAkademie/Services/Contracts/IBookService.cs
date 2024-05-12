using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IBookService
    {
        IEnumerable<Book> GetAllBooks(bool trackChanges);
        Book GetOneBookId(int  id, bool trachChanges);
        Book CreatOneBook(Book book);
        void UpdateOneBook(int id, Book book, bool trachChanges);
        void DeleteOneBook(int id, bool trachChanges);
    }
}
