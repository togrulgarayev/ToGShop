using Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.ViewModels.ProductViewModels;

namespace Business.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAllAsync();
        Task <Product> Get(int id);
        Task Create(ProductCreateViewModel productViewModel);
        //Task<ProductCreateViewModel> GetCreate();
        Task Update(int id, ProductUpdateViewModel productViewModel);
        Task Remove(int id);
    }
}
