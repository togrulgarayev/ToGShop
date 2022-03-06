using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Business.ViewModels.ProductViewModel;
using Core;

namespace ToGShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IProductImageService _productImageService;



        public ProductController(IProductService productService, ICategoryService categoryService, IProductImageService productImageService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _productImageService = productImageService;
        }
        public async Task<IActionResult> Index(int id)
        {
            var productViewModel = new ProductViewModel()
            {
                Products = await _productService.Get(id),
                ProductImages = await _productImageService.GetAllAsync(),
                Categories = await _categoryService.GetAllAsync()
            };

            return View(productViewModel);

        }
    }
}
