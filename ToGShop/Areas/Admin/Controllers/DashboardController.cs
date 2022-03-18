using System.Threading.Tasks;
using Business.Interfaces;
using Business.ViewModels.DashboardViewModel;
using Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ToGShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperModerator,Moderator")]
    public class DashboardController : Controller
    {


        private readonly IUnitOfWork _unitOfWork;

        public DashboardController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {

            var productCount = (await _unitOfWork.productRepository.GetAllAsync()).Count;
            var favouriteCount = (await _unitOfWork.productOperationsRepository.GetAllAsync(p=>p.IsFavourite==true)).Count;
            var orderCount = (await _unitOfWork.productOperationsRepository.GetAllAsync(p=>p.IsOrdered==true)).Count;
            var customerCount = (await _unitOfWork.orderRepository.GetAllAsync()).Count;


            var dashboard = new DashboardViewModel()
            {
                ProductCount = productCount,
                OrderCount = orderCount,
                FavouriteCount = favouriteCount,
                CustomerCount = customerCount
            };

            return View(dashboard);
        }
    }
}
