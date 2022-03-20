using System.Collections.Generic;
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
    }
}
