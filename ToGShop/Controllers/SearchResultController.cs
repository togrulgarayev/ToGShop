using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.ViewModels.HomeViewModel;
using Core;

namespace ToGShop.Controllers
{
    public class SearchResultController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private readonly IProductImageService _productImageService;
        private readonly IUnitOfWork _unitOfWork;



        public SearchResultController(IProductService productService, ICategoryService categoryService, IProductImageService productImageService, IBrandService brandService, IUnitOfWork unitOfWork)
        {
            _productService = productService;
            _categoryService = categoryService;
            _brandService = brandService;
            _productImageService = productImageService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var product = await _unitOfWork.productRepository.GetAllAsync();

            return View(product);
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

            return View(allQuery.ToList());
        }


    }

}
