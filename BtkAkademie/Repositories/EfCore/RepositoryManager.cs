using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EfCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly BookDbContext _Context;
        private readonly Lazy<IBookRepository> _bookRepository;

        public RepositoryManager(BookDbContext context)
        {
            _Context = context;
            _bookRepository = new Lazy<IBookRepository>(() => new BookRepository(_Context));
        }

        public IBookRepository Book => _bookRepository.Value;

        public void Save()
        {
            _Context.SaveChanges();
        }
    }
}
