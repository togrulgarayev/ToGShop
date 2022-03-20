using System.Threading.Tasks;
using Core;
using Data.DAL;
using Data.Repositories.Implementations;
using Core.Interfaces;

namespace Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;
        private IBrandRepository _brandRepository;
        private IProductImageRepository _productImageRepository;
        private IProductOperationsRepository _productOperationsRepository;
        private IProductCommentRepository _productCommentRepository;
        private IContactAdminRepository _contactAdminRepository;
        private IDiscountTimerRepository _discountTimerRepository;
        private IOrderRepository _orderRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        public IProductRepository productRepository => 
            _productRepository = _productRepository ?? new ProductRepository(_context);

        public ICategoryRepository categoryRepository =>
            _categoryRepository = _categoryRepository ?? new CategoryRepository(_context);

        public IBrandRepository brandRepository => 
            _brandRepository = _brandRepository ?? new BrandRepository(_context);

        public IProductImageRepository productImageRepository =>
            _productImageRepository = _productImageRepository ?? new ProductImageRepository(_context);

        public IProductOperationsRepository productOperationsRepository =>
            _productOperationsRepository = _productOperationsRepository ?? new ProductOperationRepository(_context);

        public IProductCommentRepository productCommentRepository =>
            _productCommentRepository = _productCommentRepository ?? new ProductCommentRepository(_context);
        public IContactAdminRepository contactAdminRepository =>
            _contactAdminRepository = _contactAdminRepository ?? new ContactAdminRepository(_context);

        public IDiscountTimerRepository discountTimerRepository =>
            _discountTimerRepository = _discountTimerRepository ?? new DiscountTimerRepository(_context);

        public IOrderRepository orderRepository =>
            _orderRepository = _orderRepository ?? new OrderRepository(_context);


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
