using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.ViewModels.FavouriteViewModels;
using Core;
using Core.Entities;
using Newtonsoft.Json;

namespace ToGShop.Controllers
{
    public class FavouriteController : Controller
    {

        private readonly IProductService _productService;
        private readonly IUnitOfWork _unitOfWork;


        public FavouriteController(IUnitOfWork unitOfWork, IProductService productService)
        {
            _unitOfWork = unitOfWork;
            _productService = productService;
        }


        public async Task<ActionResult> Favourite()
        {
            List<FavouriteViewModel> favourite = GetFavourite();
            List<FavouriteItemViewModel> model = await GetFavouriteList(favourite);
            return View(model);
        }
        


        public async Task<ActionResult> AddFavourite(int? id)
        {
            if (id == null) return NotFound();

            Product dbProduct = await _unitOfWork.productRepository.Get(p => p.Id == id);

            if (dbProduct == null) return BadRequest();

            List<FavouriteViewModel> favourite = GetFavourite();


            FavouriteViewModel productFavourite = favourite.Find(product => product.Id == dbProduct.Id);
            if (productFavourite == null)
            {
                favourite.Add(new FavouriteViewModel
                {
                    Id = dbProduct.Id,
                    Price = dbProduct.Price,
                    isDiscount = dbProduct.IsDiscount,
                    DiscountPrice = dbProduct.DiscountPrice,
                    Count = 1
                });
            }
            else
            {
                productFavourite.Count += 1;
                productFavourite.Price += productFavourite.Price;
            }


            Response.Cookies.Append("favourite",JsonConvert.SerializeObject(favourite));

            return RedirectToAction("Index", "Home");



        }

        

        private List<FavouriteViewModel> GetFavourite()
        {
            List<FavouriteViewModel> favourite;
            if (Request.Cookies["favourite"] != null)
            {
                favourite = JsonConvert.DeserializeObject<List<FavouriteViewModel>>(Request.Cookies["favourite"]);
            }
            else
            {
                favourite = new List<FavouriteViewModel>();
            }

            return favourite;
        }


        private async Task<List<FavouriteItemViewModel>> GetFavouriteList(List<FavouriteViewModel> favourite)
        {
            List<FavouriteItemViewModel> model = new List<FavouriteItemViewModel>();

            foreach (var item in favourite)
            {
                Product dbProduct = await _unitOfWork.productRepository.Get(p => p.Id == item.Id);

                FavouriteItemViewModel itemViewModel = new FavouriteItemViewModel
                {

                    Id = item.Id,
                    Name = dbProduct.Name,
                    Count = dbProduct.Count,
                    Price = item.Price,
                    isDiscount = item.isDiscount,
                    DiscountPrice = item.DiscountPrice

                };

                model.Add(itemViewModel);
            }

            return model;
        }


    }
}
