using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.ViewModels;
using Business.ViewModels.CartViewModel;
using Business.ViewModels.PaymentViewModel;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ToGShop.Controllers
{
    public class ProductOperationController : Controller
    {
        #region Injects

        private readonly IProductOperationService _productOperationService;
        private readonly IProductImageService _productImageService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private readonly UserManager<ApplicationUser> _userManager;


        public ProductOperationController(ICategoryService categoryService,IBrandService brandService,IOrderService orderService,IProductOperationService productOperationService, UserManager<ApplicationUser> userManager, IProductService productService, IProductImageService productImageService)
        {
            _orderService = orderService;
            _productOperationService = productOperationService;
            _userManager = userManager;
            _productService = productService;
            _productImageService = productImageService;
            _categoryService = categoryService;
            _brandService = brandService;
        }

        #endregion


        #region Favourite Operations

        [Authorize]
        public async Task<IActionResult> Favourite()
        {

            var userId = _userManager.GetUserId(HttpContext.User);

            var products = await _productService.GetAllAsync();
            var productOperations = await _productOperationService.GetAllFavouriteAsync(userId);
            var productImages = await _productImageService.GetAllAsync();
            var categories = await _categoryService.GetAllAsync();
            var brands = await _brandService.GetAllAsync();

            FavouriteViewModel favouriteViewModel = new FavouriteViewModel
            {
                Products = products,
                ProductOperations = productOperations,
                ProductImages = productImages,
                Categories = categories,
                Brands = brands
            };


            return View(favouriteViewModel);
        }

        [Authorize]
        public async Task<IActionResult> SetFavourite(int id)
        {

            var userId = _userManager.GetUserId(HttpContext.User);

            await _productOperationService.SetFavourite(id, userId);


            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> DeleteFavourite(int id)
        {

            var userId = _userManager.GetUserId(HttpContext.User);

            await _productOperationService.DeleteFavourite(id, userId);


            return RedirectToAction("Favourite");
        }

        #endregion


        #region Cart Operations

        [Authorize]
        public async Task<IActionResult> Cart()
        {

            var userId = _userManager.GetUserId(HttpContext.User);

            var products = await _productService.GetAllAsync();
            var productOperations = await _productOperationService.GetAllCartAsync(userId);
            var productImages = await _productImageService.GetAllAsync();
            var categories = await _categoryService.GetAllAsync();
            var brands = await _brandService.GetAllAsync();

            CartViewModel cartViewModel = new CartViewModel
            {
                Products = products,
                ProductOperations = productOperations,
                ProductImages = productImages,
                Categories = categories,
                Brands = brands
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

        #endregion


        #region Send Operations

        public async Task<IActionResult> SetSend(int id)
        {
            await _productOperationService.SetSend(id);

            return Redirect("../../Admin/UserOrder");
        }


        public async Task<IActionResult> Delete(int id)
        {
            await _productOperationService.Delete(id);

            return Redirect("../../Admin/UserSend");
        }

        #endregion


        #region PaymentOperations

        [Authorize]
        public IActionResult Payment(string crypted,decimal met)
        {

            var paymentViewModel = new PaymentViewModel
            {
                Price = met
            };

            return View(paymentViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Payment(PaymentViewModel paymentViewModel)
        {

            decimal price = paymentViewModel.Price;

            if (ModelState.IsValid)
            {
                await _orderService.Create(paymentViewModel.Order);
                return Redirect($"/Checkout?token=CfDJ8E6aunCR%2B8lDjMS%2BzxHjhpa8Is9vO9p0p4emWBxLWMiQek7emlAl9Bfxg73pAqJej8mcdn0LS5%2F%2BuOnDUDT5uM32xsrLglxxdIUytACa2FtZoZSChKPwaVoPQRqf%2FhLr6twnWjYOccgCM82SCf%2FHB8NxTx8rRQlbLLdokiGm0waW%2FBh2LYC7%2BaBvhNXjWHE8TZX7%2BRB34DK1yXoiQTcSs55za9W9dkGDzCG4ANhdMuvB4l1IqTPDNo&met={price}&key=Dn0LS5%2F%2BuOnDUDT5uM32xsrLglxxdIUytACa2FtZoZSChKPwaVoPQRqf%2FhLr6twnWjYOccgCM82SCf%2FHB8NxTx8rRQlbLLdokiGm0waW%2FBh2LYC7%2BaBvhNXjWHE8TZX7%2BRB34DK1yXoiQTcSs55za9W9dkGDzCG4ANhdMuvB4l1IqTPDNoCfDJ8E6aunCR%2B8lDjMS%2BzxHjhpa8Is9vO9p0p4emWBxLWMiQek7emlAl9Bfxg73pAqJej8mc");


            }

            return View(paymentViewModel);
        }

        #endregion


    }
}
