using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface ICategoryRepository : IRepositoryBase<Category> 
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges);
        Task<Category> GetOneCategoryByIdAsync(int id, bool trackChanges);
        void CreatOneCategory(Category category);
        void DeleteOneCategory(Category category);
        void UpdateCetegory(Category category);
    }
}
