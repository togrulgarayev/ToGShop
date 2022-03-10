using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.ViewModels.ProductImageViewModels;
using Core;
using Core.Entities;

namespace Business.Implementations
{
    public class ProductImageService : IProductImageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductImageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ProductImage>> GetAllAsync()
        {
              return await _unitOfWork.productImageRepository.GetAllAsync();
        }

        public async Task<List<ProductImage>> GetAllProductIdAsync(int id)
        {
            return await _unitOfWork.productImageRepository.GetAllAsync(pi=>pi.ProductId == id);
        }

        public async Task<ProductImage> Get(int id)
        {

            return await _unitOfWork.productImageRepository.Get(p => p.Id == id);

        }

        public async Task Create(ProductImageCreateViewModel productImageViewModel)
        {
            throw new NotImplementedException();
        }

        public async Task Update(int id, ProductImageUpdateViewModel productImageViewModel)
        {
            throw new NotImplementedException();
        }

        public async Task Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
