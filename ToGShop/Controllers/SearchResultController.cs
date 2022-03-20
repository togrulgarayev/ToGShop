using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.ViewModels.HomeViewModel;
using Core;

namespace ToGShop.Controllers
{
    public class SearchResultController : Controller
    {
        #region Injects

        private readonly IProductService _productService;
        private readonly IDiscountTimerService _discountTimerService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private readonly IProductImageService _productImageService;
        private readonly IUnitOfWork _unitOfWork;



        public SearchResultController(IDiscountTimerService discountTimerService, IProductService productService, ICategoryService categoryService, IProductImageService productImageService, IBrandService brandService, IUnitOfWork unitOfWork)
        {
            _productService = productService;
            _categoryService = categoryService;
            _brandService = brandService;
            _productImageService = productImageService;
            _unitOfWork = unitOfWork;
            _discountTimerService = discountTimerService;
        }

        #endregion

        public async Task<IActionResult> Index()
        {
            var homeViewModel = new HomeViewModel()
            {
                Categories = await _categoryService.GetAllAsync(),
                Brands = await _brandService.GetAllAsync(),
                Products = await _productService.GetAllAsync(),
                ProductImages = await _productImageService.GetAllAsync(),
                DiscountTimer = await _discountTimerService.Get()
            };

            return View(homeViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Index(string allSearch)
        {
            ViewData["SearchedAll"] = allSearch;
       
            var allQuery = from p in await _unitOfWork.productRepository.GetAllAsync() select p;

            if (!String.IsNullOrEmpty(allSearch))
            {
                allQuery = allQuery.Where(p => p.Name.Trim().ToLower().Contains(allSearch.Trim().ToLower()));
            }



            var homeViewModel = new HomeViewModel()
            {
                Categories = await _categoryService.GetAllAsync(),
                Brands = await _brandService.GetAllAsync(),
                Products = allQuery.ToList(),
                ProductImages = await _productImageService.GetAllAsync(),
                DiscountTimer = await _discountTimerService.Get()
            };

            return View(homeViewModel);
        }
    }

}
