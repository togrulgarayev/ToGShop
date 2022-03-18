using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductOperationService _productOperationService;
        private readonly IProductImageService _productImageService;
        private readonly IProductService _productService;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserSendController(IUnitOfWork unitOfWork, IProductOperationService productOperationService, IProductImageService productImageService, UserManager<ApplicationUser> userManager, IProductService productService)
        {
            _unitOfWork = unitOfWork;
            _productImageService = productImageService;
            _productOperationService = productOperationService;
            _userManager = userManager;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(HttpContext.User);

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
