using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IBookService
    {
        IEnumerable<BookDTO> GetAllBooks(bool trackChanges);
        BookDTO GetOneBookId(int  id, bool trachChanges);
        BookDTO CreatOneBook(BookForInsertionDTO bookDto);
        void UpdateOneBook(int id, BookForUpdateDTO bookDto, bool trachChanges);
        void DeleteOneBook(int id, bool trachChanges);

        (BookForUpdateDTO bookForUpdateDTO,Book book) GetOneBookForPatch(int id,bool trachChanges);

        void SaveChangesForPatch(BookForUpdateDTO bookForUpdateDTO,Book book);
    }
}
