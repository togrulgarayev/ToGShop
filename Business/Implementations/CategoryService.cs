using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.ViewModels.CategoryViewModels;
using Core;
using Core.Entities;

namespace Business.Implementations
{
    public class CategoryService:ICategoryService
    {

        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _unitOfWork.categoryRepository.GetAllAsync(c=> c.IsDeleted == false);
        }

        public async Task<Category> Get(int id)
        {
            return await _unitOfWork.categoryRepository.Get(p => p.Id == id && p.IsDeleted == false);

        }

        public async Task Create(CategoryCreateViewModel categoryViewModel)
        {
            var newCategory = new Category()
            {
                Name = categoryViewModel.Name
            };

            await _unitOfWork.categoryRepository.CreateAsync(newCategory);
            await _unitOfWork.SaveAsync();
        }

        public async Task Update(int id, CategoryUpdateViewModel categoryViewModel)
        {
            Category dbCategory = await _unitOfWork.categoryRepository.Get(c => c.Id == id);

            dbCategory.Name = categoryViewModel.Name;

            await _unitOfWork.SaveAsync();
        }

        public async Task Remove(int id)
        {
            Category dbCategory = await _unitOfWork.categoryRepository.Get(c => c.Id == id);



            _unitOfWork.categoryRepository.Remove(dbCategory);
            await _unitOfWork.SaveAsync();
        }
    }
}
