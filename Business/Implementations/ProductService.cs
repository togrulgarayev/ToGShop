using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.Utilities;
using Business.ViewModels.ProductViewModels;
using Core;
using Core.Entities;
using Microsoft.AspNetCore.Hosting;

namespace Business.Implementations
{
    public class ProductService : IProductService
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        public ProductService(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }

        public async Task<List<Product>> GetAllAsync()
        {

            return await _unitOfWork.productRepository.GetAllAsync(p=> p.IsDeleted == false, "ProductImages" , "Category");

             

        }

        public async Task<List<Product>> GetAllProductAsync()
        {
            return await _unitOfWork.productRepository.GetAllAsync();
        }

        public async Task <Product> Get(int id)
        {
            return await _unitOfWork.productRepository.Get(p=>p.Id == id && p.IsDeleted == false);
        }

        public async Task Create(ProductCreateViewModel productViewModel)
        {

            var newProduct = new Product()
            {
                Name = productViewModel.Name,
                Description = productViewModel.Description,
                Price = productViewModel.Price,
                Information = productViewModel.Information,
                Count = productViewModel.Count,
                BrandId = productViewModel.BrandId,
                CategoryId = productViewModel.CategoryId,
                IsDiscount = productViewModel.IsDiscount,
                DiscountPrice = productViewModel.DiscountPrice
            };

            await _unitOfWork.productRepository.CreateAsync(newProduct);


            await _unitOfWork.SaveAsync();


            var products = await _unitOfWork.productRepository.GetAllAsync();

            var readyProduct = products[products.Count-1];
            

            for (int i = 0; i < productViewModel.ImageFiles.Count; i++)
            {

                string filename = await productViewModel.ImageFiles[i].SaveFileAsync(_env.WebRootPath, "assets", "img");

                var productImage = new ProductImage()
                {
                    Image = filename,
                    ProductId = readyProduct.Id

                };


                await _unitOfWork.productImageRepository.CreateAsync(productImage);

                if (i==0)
                {
                    productImage.IsMain = true;
                }

                
            }




            await _unitOfWork.SaveAsync();
        }

        

        public async Task Update(int id, ProductUpdateViewModel productViewModel)
        {
            Product dbProduct = await _unitOfWork.productRepository.Get(p => p.Id == id);

            dbProduct.Name = productViewModel.Name;
            dbProduct.Description = productViewModel.Description;
            dbProduct.Price = productViewModel.Price;
            dbProduct.Information = productViewModel.Information;
            dbProduct.Count = productViewModel.Count;
            dbProduct.BrandId = productViewModel.BrandId;
            dbProduct.CategoryId = productViewModel.CategoryId;
            dbProduct.IsDiscount = productViewModel.IsDiscount;
            dbProduct.DiscountPrice = productViewModel.DiscountPrice;


            var images = await _unitOfWork.productImageRepository.GetAllAsync();

            foreach (var image in images)
            {
                if (dbProduct.Id == image.ProductId)
                {
                     _unitOfWork.productImageRepository.Remove(image);
                     await _unitOfWork.SaveAsync();
                }
            }

            await _unitOfWork.SaveAsync();

            if (productViewModel.ImageFiles != null)
            {
                for (int i = 0; i < productViewModel.ImageFiles.Count; i++)
                {
                    string filename = await productViewModel.ImageFiles[i].SaveFileAsync(_env.WebRootPath, "assets", "img");
                    var productImage = new ProductImage()
                    {
                        Image = filename,
                        ProductId = id

                    };

                    if (i==0)
                    {
                        productImage.IsMain = true;
                    }

                    await _unitOfWork.productImageRepository.CreateAsync(productImage);
                    await _unitOfWork.SaveAsync();
                }
            }
        }

        public async Task Remove(int id)
        {
            Product dbProduct = await _unitOfWork.productRepository.Get(p => p.Id == id);



             _unitOfWork.productRepository.Remove(dbProduct);
            await _unitOfWork.SaveAsync();
        }



        

    }
}
