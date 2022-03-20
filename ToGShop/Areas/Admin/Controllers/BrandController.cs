using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.Utilities;
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

        [HttpGet]
        public async Task<IActionResult> Index(string brendSearch)
        {
            ViewData["SearchedBrend"] = brendSearch;

            var brendQuery = from b in await _unitOfWork.brandRepository.GetAllAsync() select b;

            if (!String.IsNullOrEmpty(brendSearch))
            {
                brendQuery = brendQuery.Where(b => b.Name.Trim().ToLower().Contains(brendSearch.Trim().ToLower()));
            }

            return View(brendQuery.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandCreateViewModel brandViewModel)
        {

            if (ModelState.IsValid)
            {

                    if (!brandViewModel.Logo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("ImageFiles", "Seçdiyiniz fayl şəkil tipində olmalıdır ! ");
                        return View(brandViewModel);
                    }

                    if (!brandViewModel.Logo.CheckFileSize(300))
                    {
                        ModelState.AddModelError("ImageFiles", "Seçdiyiniz faylın ölçüsü 300 kb dan çox olmamalıdır !");
                        return View(brandViewModel);
                    }


                    await _brandService.Create(brandViewModel);
                    return RedirectToAction(nameof(Index));
            }



            return View(brandViewModel);
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
            if (ModelState.IsValid)
            {

                if (!brandViewModel.Logo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("ImageFiles", "Seçdiyiniz fayl şəkil tipində olmalıdır ! ");
                    return View(brandViewModel);
                }

                if (!brandViewModel.Logo.CheckFileSize(300))
                {
                    ModelState.AddModelError("ImageFiles", "Seçdiyiniz faylın ölçüsü 300 kb dan çox olmamalıdır !");
                    return View(brandViewModel);
                }


                await _brandService.Update(id ,brandViewModel);
                return RedirectToAction(nameof(Index));
            }

            
            //return RedirectToAction(nameof(Index));
            return View(brandViewModel);
        }
    }
}
