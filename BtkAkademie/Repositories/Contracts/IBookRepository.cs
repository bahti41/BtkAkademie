using Entities.Concrete;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IBookRepository:IRepositoryBase<Book>
    {
        Task<PagedList<Book>> GetAllBooksAsync(BookParameters bookParameters, bool TrackChanges);
        Task<Book> GetOneBookIdAsync(int id,bool TrackChanges);
        void CreateOneBook(Book book);
        void UpdateOneBook(Book book);
        void DeleteOneBook(Book book);

        //Versioning
        Task<List<Book>> GetAllBooksAsync(bool trachChanges);

        //İlişkisel Durum icin
        Task<IEnumerable<Book>> GetAllBooksWithDetailsAsync(bool trachChanges);
    }
}
