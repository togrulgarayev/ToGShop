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
        Task<List<ProductOperation>> GetAllCartAsync(string userId);
        Task<List<ProductOperation>> GetAllOrderedAsync(string userId);
        Task<List<ProductOperation>> GetAllSendAsync(string userId);
        Task<ProductOperation> Get(int id);
        Task SetFavourite(int productId, string userid);
        Task SetCart(int productId, string userid);
        Task SetOrdered(int productId, string userid);
        Task SetSend(int productId, string userid);
    }
}
