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
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;

        public RepositoryManager(BookDbContext context,
            IBookRepository bookRepository,
            ICategoryRepository categoryRepository)
        {
            _Context = context;
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
        }

        public IBookRepository Book => _bookRepository;

        public ICategoryRepository Category => _categoryRepository;

        public async Task SaveAsync()
        {
            await _Context.SaveChangesAsync();
        }
    }
}
