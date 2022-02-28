using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Data.Repositories.Interfaces;

namespace Data.Repositories
{
    public interface IUnitOfWork
    {
        public IProductRepository productRepository { get; }
        Task SaveAsync();
    }
}
