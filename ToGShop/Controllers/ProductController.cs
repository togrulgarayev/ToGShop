using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Business.ViewModels.ProductCommentViewModels;
using Business.ViewModels.ProductViewModel;
using Core;
using Microsoft.AspNetCore.Authorization;

namespace ToGShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IProductImageService _productImageService;
        private readonly IProductCommentService _productCommentService;



        public ProductController(IProductService productService, ICategoryService categoryService, IProductImageService productImageService, IProductCommentService productCommentService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _productImageService = productImageService;
            _productCommentService = productCommentService;
        }
        public async Task<IActionResult> Index(int id)
        {

            var productCommentViewModel = new ProductCommentViewModel()
            {
                Product = await _productService.Get(id),
                Comments = await _productCommentService.GetProductId(id),
                ProductImages = await _productImageService.GetAllAsync(),
                Categories = await _categoryService.GetAllAsync()
            };

            return View(productCommentViewModel);

        }
        

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddComment(ProductCommentViewModel productCommentViewModel)
        {
            await _productCommentService.Create(productCommentViewModel);
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> DeleteComment(int id)
        {
            await _productCommentService.Remove(id);
            return RedirectToAction("Index", "Home");
        }
    }
}
