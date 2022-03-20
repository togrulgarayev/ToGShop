using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Interfaces;
using Core;
using Core.Entities;

namespace Business.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<Order>> GetAllAsync()
        {
            return await _unitOfWork.orderRepository.GetAllAsync();
        }



        public async Task Create(Order order)
        {
            await _unitOfWork.orderRepository.CreateAsync(order);
            await _unitOfWork.SaveAsync();

        }

    }
}
