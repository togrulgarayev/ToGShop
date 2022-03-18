using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;

namespace Core
{
    public interface IUnitOfWork
    {
        public IProductRepository productRepository { get; }
        public ICategoryRepository categoryRepository { get; }
        public IBrandRepository brandRepository { get; }
        public IProductImageRepository productImageRepository { get; }
        public IProductOperationsRepository productOperationsRepository { get; }
        public IProductCommentRepository productCommentRepository { get; }
        public IContactAdminRepository contactAdminRepository { get; }
        public IDiscountTimerRepository discountTimerRepository { get; }
        public IOrderRepository orderRepository { get; }
        Task SaveAsync();
    }
}
