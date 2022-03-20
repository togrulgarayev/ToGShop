using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.Utilities;
using Business.ViewModels.DashboardViewModel;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ToGShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperModerator,Moderator")]
    public class DashboardController : Controller
    {

        
        private readonly IProductOperationService _productOperationService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private readonly IProductImageService _productImageService;
        private readonly UserManager<ApplicationUser> _userManager;

        public DashboardController(UserManager<ApplicationUser> userManager,IProductImageService productImageService,IOrderService orderService,IBrandService brandService,ICategoryService categoryService, IProductOperationService productOperationService,IProductService productService)
        {
            _orderService = orderService;
            _productOperationService = productOperationService;
            _productService = productService;
            _categoryService = categoryService;
            _brandService = brandService;
            _productImageService = productImageService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {

            var productCount = (await _productService.GetAllProductAsync()).Count;
            var favouriteCount = (await _productOperationService.GetAllFavouriteProductAsync()).Count;
            var orderCount = (await _productOperationService.GetAllOrderProductAsync()).Count;
            var customerCount = (await _orderService.GetAllAsync()).Count;



            var dashboard = new DashboardViewModel()
            {
                ProductCount = productCount,
                OrderCount = orderCount,
                FavouriteCount = favouriteCount,
                CustomerCount = customerCount,

                Categories = await _categoryService.GetAllAsync(),
                Brands = await _brandService.GetAllAsync(),
                Products = await _productService.GetAllAsync(),
                ProductImages = await _productImageService.GetAllAsync(),

                Orders = await _orderService.GetAllAsync()
            };

            return View(dashboard);
        }


        [HttpPost]
        public async Task<IActionResult> SendMessage(DashboardViewModel dashboardViewModel)
        {

            var allUsers = await _userManager.Users.ToListAsync();
            

            var adminmsg =
                    $"<body style=\"height: 100% !important;margin: 0 !important;padding: 0 !important;width: 100% !important;background-color: #f4f4f4; margin: 0 !important; padding: 0 !important;\"><div style=\"display: none; font-size: 1px; color: #fefefe; line-height: 1px; font-family: \'Lato\', Helvetica, Arial, sans-serif; max-height: 0px; max-width: 0px; opacity: 0; overflow: hidden;\"> Sizinlə əlaqə saxlamaq üçün mesajdır. </div><table style=\"border-collapse: collapse !important;\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td bgcolor=\"#6d6d6d\" align=\"center\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td align=\"center\" valign=\"top\" style=\"padding: 40px 10px 40px 10px;\"> </td></tr></table></td></tr><tr><td bgcolor=\"#6d6d6d\" align=\"center\" style=\"padding: 0px 10px 0px 10px;\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;border-collapse: collapse !important;\"><tr><td bgcolor=\"#ffffff\" align=\"center\" valign=\"top\" style=\"padding: 40px 20px 20px 20px; border-radius: 4px 4px 0px 0px; color: #111111; font-family: \'Lato\', Helvetica, Arial, sans-serif; font-size: 48px; font-weight: 400; letter-spacing: 4px; line-height: 48px;\"><h1 style=\"font-size: 48px; font-weight: 400; margin: 2;\">Xoş Gəlmisiniz</h1> <img src=\"https://www.logomaker.com/api/main/images/1j+ojFVDOMkX9Wytexe43D6khvGJqhNGmBrNwXs1M3EMoAJtliQqgPto9foz\" width=\"125\" height=\"120\" style=\"border: 0;height: auto; line-height: 100%;outline: none; text-decoration: none; display: block; border: 0px;\" /></td></tr></table></td></tr><tr><td bgcolor=\"#f4f4f4\" align=\"center\" style=\"padding: 0px 10px 0px 10px;\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;border-collapse: collapse !important;\"><tr><td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 20px 30px 40px 30px; color: #666666; font-family: \'Lato\', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\"><p style=\"margin: 0; text-align: center;\"> {@dashboardViewModel.Message} </p></td></tr><tr><td bgcolor=\"#ffffff\" align=\"left\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"border-collapse: collapse !important;\"><tr><td bgcolor=\"#ffffff\" align=\"center\" style=\"padding: 20px 30px 60px 30px;\"><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"border-collapse: collapse !important;\"></table></td></tr></table></td></tr><tr><td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 0px 30px 0px 30px; color: #666666; font-family: \'Lato\', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\"><p style=\"margin: 0; text-align: center;\">Alış-veriş zamanı səhv baş verərsə adminlə əlaqə saxlamağınız xahiş olunur !</p></td></tr><tr><td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 20px 30px 20px 30px; color: #666666; font-family: \'Lato\', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\"><p style=\"margin: 0; text-align: center;\"><a href=\"mailto:contact.togshop@gmail.com\" target=\"_blank\" style=\"color: #6d6d6d;\">contact.togshop@gmail.com</a></p></td></tr><tr><td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 0px 30px 20px 30px; color: #666666; font-family: \'Lato\', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\"><p style=\"margin: 0; text-align: center;\">Diqqətiniz üçün çox sağolun !</p></td></tr><tr><td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 0px 30px 40px 30px; border-radius: 0px 0px 4px 4px; color: #666666; font-family: \'Lato\', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\"><p style=\"margin: 0; text-align: center;\">Hörmətlə: <br>ToG Shopping ©</p></td></tr></table></td></tr><tr><td bgcolor=\"#f4f4f4\" align=\"center\" style=\"padding: 0px 10px 0px 10px;\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;border-collapse: collapse !important;\"><tr><td bgcolor=\"#f4f4f4\" align=\"left\" style=\"padding: 0px 30px 30px 30px; color: #666666; font-family: \'Lato\', Helvetica, Arial, sans-serif; font-size: 14px; font-weight: 400; line-height: 18px;\"> <br><p style=\"margin: 0; text-align: center;\">© 2022 Bütün Hüquqlar Qorunur | <a href=\"mailto:togrulgarazade@gmail.com\" target=\"_blank\" style=\"color: #111111; font-weight: 700;\"> Togrul Garazade</a>.</p></td></tr></table></td></tr></table></body>";




            var subject = "ToG Shopping - Bildiriş";


            foreach (var user in allUsers)
            {
                Helper.SendEmail(user.Email, adminmsg, subject);
            }
            
            return RedirectToAction(nameof(Index));
        }



    }
}
