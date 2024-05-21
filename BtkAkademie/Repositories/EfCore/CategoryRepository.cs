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
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(BookDbContext context) : base(context)
        {
        }

        public void CreatOneCategory(Category category) => Create(category);

        public void DeleteOneCategory(Category category) => Delete(category); 

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(c=>c.CategoryName).ToListAsync();
        }

        public async Task<Category> GetOneCategoryByIdAsync(int id, bool trackChanges)
        {
            return await FindByCondition(c=>c.CategoryId.Equals(id),trackChanges).SingleOrDefaultAsync();
        }

        public void UpdateCetegory(Category category) => Update(category);
    }
}
