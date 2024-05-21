using Entities.Concrete;
using Entities.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges);
        Task<Category> GetOneCategoryByIdAsync(int id,bool trackChanges);
        Task<Category> CreateCategoryAsync(CategoryForInsertionDTO categoryForInsertionDTO);
        Task UpdateCategoryAsync(int id, CategoryDorUpdateDTO categoryDorUpdateDTO, bool trackChanges);
        Task DaleteCategoryAsyc(int id, bool trachChanges);
    }
}
