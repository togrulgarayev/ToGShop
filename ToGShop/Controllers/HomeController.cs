using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Business.ViewModels.HomeViewModel;

namespace ToGShop.Controllers
{
    public class HomeController : Controller
    {
        #region Injects

        private readonly IProductService _productService;
        private readonly IDiscountTimerService _discountTimerService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private readonly IProductImageService _productImageService;



        public HomeController(IDiscountTimerService discountTimerService,IProductService productService, ICategoryService categoryService, IProductImageService productImageService, IBrandService brandService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _brandService = brandService;
            _productImageService = productImageService;
            _discountTimerService = discountTimerService;
        }

        #endregion

        public async Task<IActionResult> Index()
        {
            var homeViewModel = new HomeViewModel()
            {
                Categories = await _categoryService.GetAllAsync(),
                Brands = await _brandService.GetAllAsync(),
                Products = await _productService.GetAllAsync(),
                ProductImages = await _productImageService.GetAllAsync(),
                DiscountTimer = await _discountTimerService.Get()
            };

            return View(homeViewModel);
        }

        
    }
}
