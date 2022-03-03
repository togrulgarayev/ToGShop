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
        public IBrandInterface brandRepository { get; }
        Task SaveAsync();
    }
}
