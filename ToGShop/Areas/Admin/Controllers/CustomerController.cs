using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ToGShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Moderator")]
    public class CustomerController : Controller
    {
        private readonly IOrderService _orderService;


        public CustomerController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<IActionResult> Index()
        {
            var customerData = await _orderService.GetAllAsync();

            return View(customerData);
        }
    }
}
