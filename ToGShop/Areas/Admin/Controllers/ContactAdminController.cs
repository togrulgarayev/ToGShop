using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Business.Interfaces;
using Core;

namespace ToGShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactAdminController : Controller
    {

        private readonly IContactAdminService _contactAdminService;
        private readonly IUnitOfWork _unitOfWork;


        public ContactAdminController(IUnitOfWork unitOfWork, IContactAdminService contactAdminService)
        {
            _unitOfWork = unitOfWork;
            _contactAdminService = contactAdminService;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.contactAdminRepository.GetAllAsync());
        }

        public async Task<ActionResult> Delete(int id)
        {

            await _contactAdminService.Remove(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
