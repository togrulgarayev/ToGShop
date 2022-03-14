using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.ViewModels;
using Business.ViewModels.CartViewModel;
using Business.ViewModels.PaymentViewModel;
using Core;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Stripe;

namespace ToGShop.Controllers
{
    public class ProductOperationController : Controller
    {
        private readonly IProductOperationService _productOperationService;
        private readonly IProductImageService _productImageService;
        private readonly IProductService _productService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;


        public ProductOperationController(IUnitOfWork unitOfWork, IProductOperationService productOperationService, UserManager<ApplicationUser> userManager, IProductService productService, IProductImageService productImageService)
        {
            _unitOfWork = unitOfWork;
            _productOperationService = productOperationService;
            _userManager = userManager;
            _productService = productService;
            _productImageService = productImageService;
        }

        [Authorize]
        public async Task<IActionResult> Favourite()
        {

            var userId = _userManager.GetUserId(HttpContext.User);

            var products = await _productService.GetAllAsync();
            var productOperations = await _productOperationService.GetAllFavouriteAsync(userId);
            var productImages = await _productImageService.GetAllAsync();

            FavouriteViewModel favouriteViewModel = new FavouriteViewModel
            {
                Products = products,
                ProductOperations = productOperations,
                ProductImages = productImages
            };


            return View(favouriteViewModel);
        }

        [Authorize]
        public async Task<IActionResult> SetFavourite(int id)
        {

            var userId = _userManager.GetUserId(HttpContext.User);

            await _productOperationService.SetFavourite(id,userId);


            return RedirectToAction("Index","Home");
        }

        [Authorize]
        public async Task<IActionResult> DeleteFavourite(int id)
        {

            var userId = _userManager.GetUserId(HttpContext.User);

            await _productOperationService.DeleteFavourite(id, userId);


            return RedirectToAction("Favourite");
        }

        


        [Authorize]
        public async Task<IActionResult> Cart()
        {

            var userId = _userManager.GetUserId(HttpContext.User);

            var products = await _productService.GetAllAsync();
            var productOperations = await _productOperationService.GetAllCartAsync(userId);
            var productImages = await _productImageService.GetAllAsync();

            CartViewModel cartViewModel = new CartViewModel
            {
                Products = products,
                ProductOperations = productOperations,
                ProductImages = productImages
            };


            return View(cartViewModel);
        }


        [Authorize]
        public async Task<IActionResult> SetCart(int id)
        {

            var userId = _userManager.GetUserId(HttpContext.User);

            await _productOperationService.SetCart(id, userId);


            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> DeleteCart(int id)
        {

            var userId = _userManager.GetUserId(HttpContext.User);

            await _productOperationService.DeleteCart(id, userId);


            return RedirectToAction("Cart");
        }
        [Authorize]
        public IActionResult CartEmpty()
        {

            return View();
        }

        [Authorize]
        public IActionResult Payment(decimal id)
        {

            var paymentViewModel = new PaymentViewModel
            {
                Price = id
            };

            return View(paymentViewModel);
        }








    }
}
