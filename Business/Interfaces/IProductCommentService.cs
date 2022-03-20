using System.Collections.Generic;
using System.Threading.Tasks;
using Business.ViewModels.ProductCommentViewModels;
using Core.Entities;

namespace Business.Interfaces
{
    public interface IProductCommentService
    {
        Task<List<ProductComment>> GetAllAsync();
        Task<ProductComment> Get(int id);
        Task Create(ProductCommentViewModel productCommentViewModel);
        Task Remove(int id);
        Task<List<ProductComment>> GetProductId(int id);
    }
}
