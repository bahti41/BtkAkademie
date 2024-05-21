using AutoMapper;
using Entities.Concrete;
using Entities.DTOs;
using Entities.DTOs.Category;
using Entities.Exceptions;
using Entities.Exceptions.Category;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CategoryManager : ICategoryService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public CategoryManager(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<Category> CreateCategoryAsync(CategoryForInsertionDTO categoryForInsertionDTO)
        {
            var entity = _mapper.Map<Category>(categoryForInsertionDTO);
            _manager.Category.CreatOneCategory(entity);

            await _manager.SaveAsync();
            return _mapper.Map<Category>(entity);
        }

        public async Task DaleteCategoryAsyc(int id, bool trachChanges)
        {
            var entity = await _manager.Book.GetOneBookIdAsync(id, trachChanges);

            if (entity is null)
                throw new BookNotFoundException(id);

            _manager.Book.DeleteOneBook(entity);
            await _manager.SaveAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges)
        {
            return await _manager.Category.GetAllCategoriesAsync(trackChanges);
        }


        public async Task<Category> GetOneCategoryByIdAsync(int id, bool trackChanges)
        {
            var category = await _manager.Category.GetOneCategoryByIdAsync(id, trackChanges);
            if(category is null)
                throw new CategoryNotFoundException(id);
            return category;
        }

        public async Task UpdateCategoryAsync(int id, CategoryDorUpdateDTO categoryDorUpdateDTO, bool trackChanges)
        {
            var entity = await _manager.Category.GetOneCategoryByIdAsync(id, trackChanges);

            if (entity is null)
                throw new BookNotFoundException(id);

            entity = _mapper.Map<Category>(categoryDorUpdateDTO);

            _manager.Category.Update(entity);
            await _manager.SaveAsync();
        }
    }
}
