using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.ViewModels.UserOrderViewModel;
using Microsoft.AspNetCore.Authorization;

namespace ToGShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Moderator")]
    public class UserOrderController : Controller
    {
        private readonly IProductOperationService _productOperationService;
        private readonly IProductImageService _productImageService;
        private readonly IProductService _productService;


        public UserOrderController(IProductOperationService productOperationService, IProductImageService productImageService, IProductService productService)
        {
            _productImageService = productImageService;
            _productOperationService = productOperationService;
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            

            var products = await _productService.GetAllAsync();
            var productOperationsOrders = await _productOperationService.GetAllProductOrderedAsync();
            var productImages = await _productImageService.GetAllAsync();

            UserOrderViewModel userOrderViewModel = new UserOrderViewModel
            {
                Products = products,
                ProductOperationsOrders = productOperationsOrders,
                ProductImages = productImages
            };


            return View(userOrderViewModel);
        }
    }
}
