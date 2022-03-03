using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Business.Interfaces;
using Business.ViewModels;
using Core;

namespace ToGShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IUnitOfWork _unitOfWork;


        public ProductController(IUnitOfWork unitOfWork, IProductService productService)
        {
            _unitOfWork = unitOfWork;
            _productService = productService;
        }


        public async Task<ActionResult> Index()
        {
            return View(await _unitOfWork.productRepository.GetAllAsync());
        }



        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductCreateViewModel productViewModel)
        {

            if (!ModelState.IsValid)
            {

                await _productService.Create(productViewModel);
            }
            else
            {
                return View(productViewModel);
            }


            return RedirectToAction(nameof(Index));
        }


        public async Task<ActionResult> Update()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            await _productService.Remove(id);

            return RedirectToAction(nameof(Index));
        }



    }
}
