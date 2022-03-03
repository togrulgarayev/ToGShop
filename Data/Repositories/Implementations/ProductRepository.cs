using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Data.DAL;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Implementations
{
    public class ProductRepository : Repository<Product> , IProductRepository
    {

        public ProductRepository(AppDbContext context):base(context)
        {
            _context = context;
        }

        private readonly AppDbContext _context;
    }
}
