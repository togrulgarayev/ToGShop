using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.ViewModels.CategoryViewModels;
using Core;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;

namespace ToGShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperModerator")]
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

        [HttpGet]
        public async Task<IActionResult> Index(string categorySearch)
        {
            ViewData["SearchedCategory"] = categorySearch;

            var categoryQuery = from c in await _unitOfWork.categoryRepository.GetAllAsync() select c;

            if (!String.IsNullOrEmpty(categorySearch))
            {
                categoryQuery = categoryQuery.Where(c => c.Name.Trim().ToLower().Contains(categorySearch.Trim().ToLower()));
            }

            return View(categoryQuery.ToList());
        }


        public IActionResult Create()
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

        public async Task<ActionResult> Update(int id)
        {
            Category category = await _categoryService.Get(id);

            if (category == null) return NotFound();


            var categoryViewModel = new CategoryUpdateViewModel()
            {
                Name = category.Name

            };



            return View(categoryViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(int id, CategoryUpdateViewModel categoryViewModel)
        {
            await _categoryService.Update(id, categoryViewModel);
            return RedirectToAction(nameof(Index));
        }
    }
}
