using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository:IRepository<Product>
    {
    }
}
