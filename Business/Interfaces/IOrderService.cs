using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Business.Interfaces
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllAsync();
        Task Create(Order order);
    }
}
