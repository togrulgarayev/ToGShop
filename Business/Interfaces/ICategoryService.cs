using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Business.ViewModels.CategoryViewModels;
using Core.Entities;

namespace Business.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();
        Task <Category> Get(int id);
        Task Create(CategoryCreateViewModel categoryViewModel);
        Task Update(int id, CategoryUpdateViewModel categoryViewModel);
        Task Remove(int id);
    }
}
