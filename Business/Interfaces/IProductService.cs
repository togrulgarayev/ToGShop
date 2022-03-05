using System.Collections.Generic;
using System.Threading.Tasks;
using Business.ViewModels;
using Core.Entities;

namespace Business.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAllAsync();
        Task Create(ProductCreateViewModel productViewModel);
        Task<ProductCreateViewModel> GetCreate();
        Task Update(int id, ProductUpdateViewModel productViewModel);
        Task Remove(int id);
    }
}
