using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.ViewModels.CheckoutViewModel;
using Core;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Stripe;

namespace ToGShop.Controllers
{
    public class CheckoutController : Controller
    {


        #region Injects

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductOperationService _productOperationService;
        private readonly ICategoryService _categoryService;


        public CheckoutController(ICategoryService categoryService,UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IProductOperationService productOperationService)
        {

            _productOperationService = productOperationService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _categoryService = categoryService;
        }

        #endregion

        [TempData]
        public string TotalAmount { get; set; }
        public async Task<IActionResult> Index(string crypted, decimal met)
        {

            var price = met;

            ViewBag.DollarAmount = price;
            ViewBag.total = Math.Round(ViewBag.DollarAmount, 2) * 100;
            ViewBag.total = Convert.ToInt64(ViewBag.total);
            long total = ViewBag.total;
            TotalAmount = total.ToString();




            var categories = await _categoryService.GetAllAsync();

            var checkoutViewModel = new CheckoutViewModel()
            {
                Categories = categories
            };

            return View(checkoutViewModel);
        }

        


        [HttpPost]
        public async Task<IActionResult> Processing(string stripeToken, string stripeEmail)
        {

            var userId = _userManager.GetUserId(HttpContext.User);
            

            foreach (var item in await _unitOfWork.productOperationsRepository.GetAllAsync())
            {
                if (item.ApplicationUserId==userId && item.InCart == true)
                {
                    item.IsOrdered = true;
                    item.InCart = false;
                }

                _unitOfWork.productOperationsRepository.Update(item);
                await _unitOfWork.SaveAsync();
            }




            var user = _userManager.GetUserName(HttpContext.User);


            var optionsCust = new CustomerCreateOptions
            {
                Email = stripeEmail,
                Name = user,
                Phone = "04-234567"

            };
            var serviceCust = new CustomerService();
            Customer customer = serviceCust.Create(optionsCust);
            var optionsCharge = new ChargeCreateOptions
            {
                /*Amount = HttpContext.Session.GetLong("Amount")*/
                Amount = Convert.ToInt64(TempData["TotalAmount"]),
                Currency = "AZN",
                Description = "Onlayn Sifariş - ToG Shopping",
                Source = stripeToken,
                ReceiptEmail = stripeEmail,

            };
            var service = new ChargeService();
            Charge charge = service.Create(optionsCharge);
            if (charge.Status == "succeeded")
            {
                string BalanceTransactionId = charge.BalanceTransactionId;
                ViewBag.AmountPaid = Convert.ToDecimal(charge.Amount) % 100 / 100 + (charge.Amount) / 100;
                ViewBag.BalanceTxId = BalanceTransactionId;
                ViewBag.Customer = customer.Name;
                //return View();
            }



            var categories = await _categoryService.GetAllAsync();

            var checkoutViewModel = new CheckoutViewModel()
            {
                Categories = categories
            };

            return View(checkoutViewModel);




            //return View();
        }
    }
}
