﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.ViewModels.BrandViewModels;
using Core;
using Core.Entities;

namespace ToGShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;
        private readonly IUnitOfWork _unitOfWork;


        public BrandController(IUnitOfWork unitOfWork, IBrandService brandService)
        {
            _unitOfWork = unitOfWork;
            _brandService = brandService;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.brandRepository.GetAllAsync());
        }


        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandCreateViewModel brandViewModel)
        {

            await _brandService.Create(brandViewModel);

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {

            await _brandService.Remove(id);

            return RedirectToAction(nameof(Index));
        }


        public async Task<ActionResult> Update(int id)
        {
            Brand brand = await _brandService.Get(id);

            if (brand == null) return NotFound();


            var brandViewModel = new BrandUpdateViewModel()
            {
                Name = brand.Name

            };



            return View(brandViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(int id, BrandUpdateViewModel brandViewModel)
        {

            await _brandService.Update(id, brandViewModel);
            return RedirectToAction(nameof(Index));
        }
    }
}
