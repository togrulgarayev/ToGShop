using Core.Entities;
using Core.Interfaces;
using Data.DAL;

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
