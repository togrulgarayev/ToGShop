using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Business.Interfaces;
using Business.ViewModels;
using Core;
using Core.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;

namespace Business.Implementations
{
    public class ProductService : IProductService
    {
        
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Product>> GetAllAsync()
        {

            return await _unitOfWork.productRepository.GetAllAsync(p=> p.IsDeleted == false);

             

        }

        public async Task Create(ProductCreateViewModel productViewModel)
        {


            var newProduct = new Product(){

                Name = productViewModel.Name,
                Description = productViewModel.Description,
                Price = productViewModel.Price,
                Information = productViewModel.Information,
                Count = productViewModel.Count,
                BrandId = productViewModel.BrandId,
                CategoryId = productViewModel.CategoryId



            };

            await _unitOfWork.productRepository.CreateAsync(newProduct);

            await _unitOfWork.SaveAsync();
        }

        public async Task<ProductCreateViewModel> GetCreate()
        {
            var myCategory = await _unitOfWork.categoryRepository.GetAllAsync(c => c.IsDeleted == false);
            var myBrand = await _unitOfWork.brandRepository.GetAllAsync(b => b.IsDeleted == false);

            var productCreateViewModel = new ProductCreateViewModel()
            {
                Categories = myCategory,
                Brands = myBrand
            };


            return productCreateViewModel;
        }

        public Task Update(int id, ProductUpdateViewModel productViewModel)
        {
            throw new NotImplementedException();
        }

        public async Task Remove(int id)
        {
            Product dbProduct = await _unitOfWork.productRepository.Get(p => p.Id == id);



             _unitOfWork.productRepository.Remove(dbProduct);
            await _unitOfWork.SaveAsync();
        }
    }
}
