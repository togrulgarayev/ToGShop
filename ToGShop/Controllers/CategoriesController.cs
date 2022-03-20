using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.ViewModels.CategoriesViewModel;

namespace ToGShop.Controllers
{
    public class CategoriesController : Controller
    {
        #region Injects

        private readonly IProductService _productService;
        private readonly IBrandService _brandService;
        private readonly ICategoryService _categoryService;
        private readonly IProductImageService _productImageService;



        public CategoriesController(IBrandService brandService, IProductService productService, ICategoryService categoryService, IProductImageService productImageService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _productImageService = productImageService;
            _brandService = brandService;
        }

        #endregion


        public async Task<IActionResult> Index(int id)
        {

            var categoriesViewModel = new CategoriesViewModel()
            {
                Products = await _productService.GetAllAsync(),
                ProductImages = await _productImageService.GetAllAsync(),
                Categories = await _categoryService.GetAllAsync(),
                Brands = await _brandService.GetAllAsync()
            };

            return View(categoriesViewModel);

        }
    }
}
