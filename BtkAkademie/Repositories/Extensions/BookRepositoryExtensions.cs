using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Extensions
{
    public static class BookRepositoryExtensions
    {
        // Price Max And Min Filter İşlemi
        public static IQueryable<Book> FilterBooks(this IQueryable<Book> books, uint minPrice, uint maxPrice) =>
            books.Where(book => book.Price >= minPrice && book.Price <= maxPrice);

        // Gelen Breaking Point'i Kücük Harfe Cevirme ve Title İle seacrh İşlemi Yapar
        public static IQueryable<Book> Search(this IQueryable<Book> books,string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return books;
            
            var lowerCaseTerm = searchTerm.Trim().ToLower(); 
            return books
                .Where(b => b.Title
                .ToLower()
                .Contains(searchTerm));
        } 

        // Gelen Verileri Harf sırasına göre sıralama İşlemi(Book icin)
        public static IQueryable<Book> Sort(this IQueryable<Book> books,string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return books.OrderBy(b=>b.Id);

            var orderQuery = OrderQueryBuilder.CreatOrderQuery<Book>(orderByQueryString);

            if (orderQuery is null)
                return books.OrderBy(b => b.Id);

            return books.OrderBy(orderQuery);
        }
    }
}
