using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.ViewModels.CategoryViewModels;
using Core;

namespace ToGShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IUnitOfWork _unitOfWork;


        public CategoryController(IUnitOfWork unitOfWork, ICategoryService categoryService)
        {
            _unitOfWork = unitOfWork;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.categoryRepository.GetAllAsync());
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateViewModel categoryViewModel)
        {

            await _categoryService.Create(categoryViewModel);

            return RedirectToAction(nameof(Index));
        }


        public async Task<ActionResult> Delete(int id)
        {

            await _categoryService.Remove(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
