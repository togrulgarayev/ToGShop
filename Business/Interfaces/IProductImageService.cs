using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Business.ViewModels.ProductImageViewModels;
using Core.Entities;

namespace Business.Interfaces
{
    public interface IProductImageService
    {
        Task<List<ProductImage>> GetAllAsync();
        Task<List<ProductImage>> GetAllProductIdAsync(int id);
        Task <ProductImage> Get(int id);
        Task Create(ProductImageCreateViewModel productImageViewModel);
        //Task<ProductCreateViewModel> GetCreate();
        Task Update(int id, ProductImageUpdateViewModel productImageViewModel);
        Task Remove(int id);
    }
}
