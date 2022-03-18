using Business.Interfaces;
using Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Business.ViewModels.HomeViewModel;

namespace ToGShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly IDiscountTimerService _discountTimerService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private readonly IProductImageService _productImageService;
        private readonly IUnitOfWork _unitOfWork;



        public HomeController(IDiscountTimerService discountTimerService,IProductService productService, ICategoryService categoryService, IProductImageService productImageService, IBrandService brandService, IUnitOfWork unitOfWork)
        {
            _productService = productService;
            _categoryService = categoryService;
            _brandService = brandService;
            _productImageService = productImageService;
            _unitOfWork = unitOfWork;
            _discountTimerService = discountTimerService;
        }

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

        
    }
}
