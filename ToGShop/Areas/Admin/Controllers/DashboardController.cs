using System.Threading.Tasks;
using Business.Interfaces;
using Business.ViewModels.DashboardViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ToGShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperModerator,Moderator")]
    public class DashboardController : Controller
    {

        
        private readonly IProductOperationService _productOperationService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private readonly IProductImageService _productImageService;

        public DashboardController(IProductImageService productImageService,IOrderService orderService,IBrandService brandService,ICategoryService categoryService, IProductOperationService productOperationService,IProductService productService)
        {
            _orderService = orderService;
            _productOperationService = productOperationService;
            _productService = productService;
            _categoryService = categoryService;
            _brandService = brandService;
            _productImageService = productImageService;
        }

        public async Task<IActionResult> Index()
        {

            var productCount = (await _productService.GetAllProductAsync()).Count;
            var favouriteCount = (await _productOperationService.GetAllFavouriteProductAsync()).Count;
            var orderCount = (await _productOperationService.GetAllOrderProductAsync()).Count;
            var customerCount = (await _orderService.GetAllAsync()).Count;



            var dashboard = new DashboardViewModel()
            {
                ProductCount = productCount,
                OrderCount = orderCount,
                FavouriteCount = favouriteCount,
                CustomerCount = customerCount,

                Categories = await _categoryService.GetAllAsync(),
                Brands = await _brandService.GetAllAsync(),
                Products = await _productService.GetAllAsync(),
                ProductImages = await _productImageService.GetAllAsync(),

                Orders = await _orderService.GetAllAsync()
            };

            return View(dashboard);
        }
    }
}
