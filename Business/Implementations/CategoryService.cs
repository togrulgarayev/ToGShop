using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.ViewModels;
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
            throw new NotImplementedException();
        }

        public async Task Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
