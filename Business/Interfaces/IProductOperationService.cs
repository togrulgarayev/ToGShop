using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Business.Interfaces
{
    public interface IProductOperationService
    {
        Task<List<ProductOperation>> GetAllAsync(string userId);
        Task<List<ProductOperation>> GetAllFavouriteAsync(string userId);
        Task<List<ProductOperation>> GetAllFavouriteProductAsync();
        Task<List<ProductOperation>> GetAllCartAsync(string userId);
        Task<List<ProductOperation>> GetAllProductOrderedAsync();
        Task<List<ProductOperation>> GetAllProductSendAsync();
        Task<List<ProductOperation>> GetAllOrderedAsync(string userId);
        Task<List<ProductOperation>> GetAllOrderedSendAsync(string userId);
        Task<List<ProductOperation>> GetAllSendAsync(string userId);
        Task<ProductOperation> Get(int id);
        Task SetFavourite(int productId, string userid);
        Task SetCart(int productId, string userid);
        Task SetSend(int id);
        Task Delete(int id);

        Task DeleteFavourite(int productId, string userid);
        Task DeleteCart(int productId, string userid);

        
    }
}
