using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.ViewModels.PaymentViewModel;
using Core;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Stripe;

namespace ToGShop.Controllers
{
    public class CheckoutController : Controller
    {


        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductOperationService _productOperationService;


        public CheckoutController(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IProductOperationService productOperationService)
        {

            _productOperationService = productOperationService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        [TempData]
        public string TotalAmount { get; set; }
        public async Task<IActionResult> Index(decimal met)
        {

            var price = met;

            ViewBag.DollarAmount = price;
            ViewBag.total = Math.Round(ViewBag.DollarAmount, 2) * 100;
            ViewBag.total = Convert.ToInt64(ViewBag.total);
            long total = ViewBag.total;
            TotalAmount = total.ToString();


            return View();
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



            




            return View();
        }
    }
}
