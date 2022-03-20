using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Business.ViewModels.ProductCommentViewModels;
using Microsoft.AspNetCore.Authorization;

namespace ToGShop.Controllers
{
    public class ProductController : Controller
    {
        #region Injects

        private readonly IProductService _productService;
        private readonly IBrandService _brandService;
        private readonly ICategoryService _categoryService;
        private readonly IProductImageService _productImageService;
        private readonly IProductCommentService _productCommentService;



        public ProductController(IBrandService brandService, IProductService productService, ICategoryService categoryService, IProductImageService productImageService, IProductCommentService productCommentService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _productImageService = productImageService;
            _productCommentService = productCommentService;
            _brandService = brandService;
        }

        #endregion


        public async Task<IActionResult> Index(int id)
        {

            var productCommentViewModel = new ProductCommentViewModel()
            {
                Product = await _productService.Get(id),
                Comments = await _productCommentService.GetProductId(id),
                ProductImages = await _productImageService.GetAllAsync(),
                Categories = await _categoryService.GetAllAsync(),
                Brands = await _brandService.GetAllAsync()
            };

            return View(productCommentViewModel);

        }


        #region Comments

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddComment(ProductCommentViewModel productCommentViewModel)
        {
            await _productCommentService.Create(productCommentViewModel);
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> DeleteComment(int id, string ReturnUrl)
        {
            await _productCommentService.Remove(id);


            if (ReturnUrl != null)
            {

                return Redirect(ReturnUrl);
            }

            return RedirectToAction("Index", "Home");
        }



        #endregion
    }
}
