using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.ViewModels.UserSendViewModel;
using Core;
using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace ToGShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserSendController : Controller
    {
        private readonly IProductOperationService _productOperationService;
        private readonly IProductImageService _productImageService;
        private readonly IProductService _productService;

        public UserSendController(IProductOperationService productOperationService, IProductImageService productImageService, IProductService productService)
        {
            _productImageService = productImageService;
            _productOperationService = productOperationService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {

            var products = await _productService.GetAllAsync();
            var productOperationsOrders = await _productOperationService.GetAllProductSendAsync();
            var productImages = await _productImageService.GetAllAsync();

            UserSendViewModel userOrderViewModel = new UserSendViewModel
            {
                Products = products,
                ProductOperationsOrders = productOperationsOrders,
                ProductImages = productImages
            };


            return View(userOrderViewModel);
        }
    }
}
